import * as controller from 'hr.controller';
import * as PageStart from 'clientlibs.PageStart';
import * as HalClient from 'clientlibs.HalEndpointClient';
import * as iter from 'hr.iterable';
import * as jsonEditor from 'clientlibs.json-editor-plugin';

interface HalLinkDisplay {
    href: string,
    rel: string,
    method: string,
    getClient(): HalClient.HalEndpointClient<any>;
}

interface HalDataDisplay {
    raw: string
}

interface HalRequestData {
    jsonData: string;
}

interface HalEndpointDoc {
    requestSchema: any,
    responseSchema: any,
}

var defaultError = { path: null };

export class LinkController {
    public static Builder(parentController: HalcyonBrowserController) {
        return new controller.ControllerBuilder<LinkController, HalcyonBrowserController, HalLinkDisplay>(LinkController, parentController);
    }

    private rel: string;
    private parentController: HalcyonBrowserController;
    private client: HalClient.HalEndpointClient<any>;
    private formModel = null;
    private jsonEditor;
    private currentError: Error = null;

    constructor(bindings: controller.BindingCollection, parentController: HalcyonBrowserController, link: HalLinkDisplay) {
        this.rel = link.rel;
        this.parentController = parentController;
        this.client = link.getClient();

        if (link.method != "GET" && this.client.HasLinkDoc(this.rel)) {
            this.client.LoadLinkDoc<HalEndpointDoc>(this.rel)
                .then(docClient => {
                    var doc = docClient.GetData();
                    if (doc.requestSchema) {
                        this.formModel = jsonEditor.create<any>(bindings.getHandle("editorHolder"), {
                            schema: doc.requestSchema,
                            disable_edit_json: true,
                            disable_properties: true,
                            disable_collapse: true,
                            show_errors: "always",
                            custom_validators: [
                                (schema, value, path) => this.showCurrentErrorValidator(schema, value, path)
                            ],
                        });
                        this.jsonEditor = this.formModel.getEditor();
                        this.formModel.setData(this.client.GetData());
                    }
                });
        }
    }

    submit(evt) {
        evt.preventDefault();
        if (this.formModel != null) {
            var data = this.formModel.getData();
            this.client.LoadLinkWith(this.rel, data)
                .then(result => {
                    if (result.HasLink("self")) {
                        var link = result.GetLink("self");
                        if (link.method == "GET") {
                            window.location.href = "/?entry=" + encodeURIComponent(link.href);
                        }
                    }
                    else {
                        window.location.href = window.location.href;
                    }
                })
                .catch(err => {
                    this.currentError = err;
                    this.jsonEditor.onChange();
                });
        }
        else {
            throw new Error("No form model set for link " + this.rel);
        }
    }

    private showCurrentErrorValidator(schema, value, path): any {
        if (this.currentError !== null) {
            if (path === "root") {
                return {
                    path: path,
                    message: this.currentError.message
                }
            }

            if (this.currentError instanceof HalClient.HalError) {
                var halError = <HalClient.HalError>this.currentError;

                //walk path to error
                var shortPath = this.errorPath(path);
                var errorMessage = halError.getValidationError(shortPath);
                if (errorMessage !== undefined) {
                    //Listen for changes on field
                    //this.fieldWatcher.watch(path, shortPath, this.currentError);
                    return {
                        path: path,
                        message: errorMessage
                    };
                }
            }
        }
        return defaultError;
    }

    private errorPath(path) {
        return path.replace('root.', '');
    }
}

export class HalcyonBrowserController {
    public static Builder() {
        return new controller.ControllerBuilder<HalcyonBrowserController, void, void>(HalcyonBrowserController);
    }

    private linkModel: controller.Model<HalLinkDisplay>;
    private embedsModel: controller.Model<HalClient.Embed<any>>;
    private dataModel: controller.Model<any>;
    private client: HalClient.HalEndpointClient<any>;

    constructor(bindings: controller.BindingCollection) {
        this.linkModel = bindings.getModel<HalLinkDisplay>("links");
        this.embedsModel = bindings.getModel<HalClient.Embed<any>>("embeds");
        this.dataModel = bindings.getModel<any>("data");
    }

    showResults(client: HalClient.HalEndpointClient<any>) {
        this.client = client;

        var dataString = JSON.stringify(client.GetData(), null, 4);
        this.dataModel.setData(dataString);

        var linkControllerBuilder = LinkController.Builder(this);
        var iterator: iter.IterableInterface<HalClient.HalLinkInfo> = new iter.Iterable(client.GetAllLinks());
        var linkIter = iterator.select<HalLinkDisplay>(i => this.getLinkDisplay(i));
        this.linkModel.setData(linkIter, linkControllerBuilder.createOnCallback(), this.getLinkVariant);

        var embedsBuilder = HalcyonEmbedsController.Builder();
        this.embedsModel.setData(client.GetAllEmbeds(), embedsBuilder.createOnCallback());
    }

    getCurrentClient() {
        return this.client;
    }

    private getLinkDisplay(i: HalClient.HalLinkInfo) {
        var link: HalLinkDisplay = {
            rel: i.rel,
            href: i.href,
            method: i.method,
            getClient: () => this.client,
        };
        if (i.method === "GET") {
            link.href = '/?entry=' + encodeURIComponent(i.href);
        }
        return link;
    }

    private getLinkVariant(item: HalLinkDisplay) {
        if (item.method !== "GET") {
            return "form";
        }
    }
}

class HalcyonSubBrowserController extends HalcyonBrowserController {
    public static SubBrowserBuilder() {
        return new controller.ControllerBuilder<HalcyonSubBrowserController, void, HalClient.HalEndpointClient<any>>(HalcyonSubBrowserController);
    }

    constructor(bindings: controller.BindingCollection, context: void, data: HalClient.HalEndpointClient<any>) {
        super(bindings);
        this.showResults(data);
    }
}

class HalcyonEmbedsController {
    public static Builder() {
        return new controller.ControllerBuilder<HalcyonEmbedsController, void, HalClient.Embed<any>>(HalcyonEmbedsController);
    }

    constructor(bindings: controller.BindingCollection, context: void, data: HalClient.Embed<any>) {
        var itemModel = bindings.getModel<HalClient.HalEndpointClient<any>>("items");
        var subBrowserBuilder = HalcyonSubBrowserController.SubBrowserBuilder();
        itemModel.setData(data.GetAllClients(), subBrowserBuilder.createOnCallback());
    }
}
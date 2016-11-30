import * as controller from 'hr.controller';
import * as PageStart from 'clientlibs.PageStart';
import * as HalClient from 'clientlibs.HalEndpointClient';
import * as iter from 'hr.iterable';

interface HalLinkDisplay {
    href: string,
    rel: string
}

interface HalDataDisplay {
    raw: string
}

export class LinkController {
    public static Builder(parentController: HalcyonBrowserController) {
        return new controller.ControllerBuilder<LinkController, HalcyonBrowserController, HalClient.HalLinkInfo>(LinkController, parentController);
    }

    private rel: string;
    private parentController: HalcyonBrowserController;

    constructor(bindings: controller.BindingCollection, parentController: HalcyonBrowserController, link: HalClient.HalLinkInfo) {
        this.rel = link.rel;
        this.parentController = parentController;
    }

    visit(evt) {
        evt.preventDefault();
        //Show loading here
        this.parentController.getCurrentClient().LoadLink<any>(this.rel)
            .then(client => {
                this.parentController.showResults(client);
            });

    }
}

export class HalcyonBrowserController {
    public static Builder() {
        return new controller.ControllerBuilder<HalcyonBrowserController, void, void>(HalcyonBrowserController);
    }

    private linkModel: controller.Model<iter.IterableInterface<HalLinkDisplay>>;
    //private linkModel: controller.Model<HalLinkDisplay[]>;
    private embedsModel: controller.Model<HalClient.Embed<any>[]>;
    private dataModel: controller.Model<string>;
    private client: HalClient.HalEndpointClient<any>;

    constructor(bindings: controller.BindingCollection) {
        this.linkModel = bindings.getModel<iter.IterableInterface<HalLinkDisplay>>("links");
        //this.linkModel = bindings.getModel<HalLinkDisplay[]>("links");
        this.embedsModel = bindings.getModel<HalClient.Embed<any>[]>("embeds");
        this.dataModel = bindings.getModel<string>("data");
    }

    showResults(client: HalClient.HalEndpointClient<any>) {
        this.client = client;

        this.dataModel.setData(JSON.stringify(client.GetData()));

        var linkControllerBuilder = LinkController.Builder(this);
        var iterator: iter.IterableInterface<HalClient.HalLinkInfo> = new iter.Iterable(client.GetAllLinks());
        iterator = iterator.select<HalLinkDisplay>(i => {
            var link: HalLinkDisplay = {
                rel: i.rel,
                href: '/?entry=' + encodeURIComponent(i.href)
            };
            return link;
        });
        this.linkModel.setData(iterator, linkControllerBuilder.createOnCallback());
        //this.linkModel.setData(client.GetAllLinks(), linkControllerBuilder.createOnCallback());

        var embedsBuilder = HalcyonEmbedsController.Builder();
        this.embedsModel.setData(client.GetAllEmbeds(), embedsBuilder.createOnCallback());
    }

    getCurrentClient() {
        return this.client;
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
        var itemModel = bindings.getModel<HalClient.HalEndpointClient<any>[]>("items");
        var subBrowserBuilder = HalcyonSubBrowserController.SubBrowserBuilder();
        itemModel.setData(data.GetAllClients(), subBrowserBuilder.createOnCallback());
    }
}
﻿import * as controller from 'hr.controller';
import * as PageStart from 'clientlibs.PageStart';
import * as HalClient from 'clientlibs.HalEndpointClient';
import * as iter from 'hr.iterable';

interface HalLinkDisplay {
    href: string,
    rel: string,
    method: string
}

interface HalDataDisplay {
    raw: string
}

interface HalRequestData {
    jsonData: string;
}

export class LinkController {
    public static Builder(parentController: HalcyonBrowserController) {
        return new controller.ControllerBuilder<LinkController, HalcyonBrowserController, HalClient.HalLinkInfo>(LinkController, parentController);
    }

    private rel: string;
    private parentController: HalcyonBrowserController;
    private requestDataModel: controller.Model<HalRequestData>;

    constructor(bindings: controller.BindingCollection, parentController: HalcyonBrowserController, link: HalClient.HalLinkInfo) {
        this.rel = link.rel;
        this.parentController = parentController;
        this.requestDataModel = bindings.getModel<HalRequestData>("requestData");
    }

    submit(evt) {
        evt.preventDefault();
        var data = this.requestDataModel.getData();
        alert(data.jsonData);
    }
}

export class HalcyonBrowserController {
    public static Builder() {
        return new controller.ControllerBuilder<HalcyonBrowserController, void, void>(HalcyonBrowserController);
    }

    private linkModel: controller.Model<HalLinkDisplay>;
    //private linkModel: controller.Model<HalLinkDisplay[]>;
    private embedsModel: controller.Model<HalClient.Embed<any>>;
    private dataModel: controller.Model<any>;
    private client: HalClient.HalEndpointClient<any>;

    constructor(bindings: controller.BindingCollection) {
        this.linkModel = bindings.getModel<HalLinkDisplay>("links");
        //this.linkModel = bindings.getModel<HalLinkDisplay[]>("links");
        this.embedsModel = bindings.getModel<HalClient.Embed<any>>("embeds");
        this.dataModel = bindings.getModel<any>("data");
    }

    showResults(client: HalClient.HalEndpointClient<any>) {
        this.client = client;

        var dataString = JSON.stringify(client.GetData(), null, 4);
        this.dataModel.setData(dataString);

        var linkControllerBuilder = LinkController.Builder(this);
        var iterator: iter.IterableInterface<HalClient.HalLinkInfo> = new iter.Iterable(client.GetAllLinks());
        iterator = iterator.select<HalLinkDisplay>(i => {
            var link: HalLinkDisplay = {
                rel: i.rel,
                href: '/?entry=' + encodeURIComponent(i.href),
                method: i.method
            };
            return link;
        });
        this.linkModel.setData(iterator, linkControllerBuilder.createOnCallback(), this.getLinkVariant);
        //this.linkModel.setData(client.GetAllLinks(), linkControllerBuilder.createOnCallback());

        var embedsBuilder = HalcyonEmbedsController.Builder();
        this.embedsModel.setData(client.GetAllEmbeds(), embedsBuilder.createOnCallback());
    }

    getCurrentClient() {
        return this.client;
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
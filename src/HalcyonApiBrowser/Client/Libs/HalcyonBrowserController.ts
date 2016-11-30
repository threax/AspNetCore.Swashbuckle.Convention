import * as controller from 'hr.controller';
import * as PageStart from 'clientlibs.PageStart';
import * as HalClient from 'clientlibs.HalEndpointClient';
import * as iter from 'hr.iterable';

interface HalLinkDisplay {
    href: string,
    rel: string
}

export class HalcyonBrowserController {
    public static Builder(pageStart: PageStart.PageStart) {
        return new controller.ControllerBuilder<HalcyonBrowserController, PageStart.PageStart, void>(HalcyonBrowserController, pageStart);
    }

    private linkModel: controller.Model<HalLinkDisplay[]>;

    constructor(bindings: controller.BindingCollection, context: PageStart.PageStart) {
        this.linkModel = bindings.getModel<HalLinkDisplay[]>("links");
        this.showResults(context.EntryPoint);
    }

    showResults(client: HalClient.HalEndpointClient<any>) {
        this.linkModel.setData(client.GetLinks().GetAllLinks());
    }
}
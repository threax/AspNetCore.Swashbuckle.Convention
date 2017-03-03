import * as hal from 'hr.halcyon.EndpointClient';
import { Fetcher } from 'hr.fetcher';

export class EntryPointsResult {
    private client: hal.HalEndpointClient;

    constructor(client: hal.HalEndpointClient) {
        this.client = client;
    }

    public get data(): EntryPoints {
        return this.client.GetData<EntryPoints>();
    }


    public refresh(): Promise<EntryPointsResult> {
        return this.client.LoadLink("self")
            .then(r => {
                return new EntryPointsResult(r);
            });
    }

    public canRefresh(): boolean {
        return this.client.HasLink("self");
    }


    public listThingies(query: CollectionQuery): Promise<ThingyCollectionViewResult> {
        return this.client.LoadLinkWithQuery("listThingies", query)
            .then(r => {
                return new ThingyCollectionViewResult(r);
            });
    }

    public canListThingies(): boolean {
        return this.client.HasLink("listThingies");
    }

}

export class SubThingyCollectionViewResult {
    private client: hal.HalEndpointClient;

    constructor(client: hal.HalEndpointClient) {
        this.client = client;
    }

    public get data(): SubThingyCollectionView {
        return this.client.GetData<SubThingyCollectionView>();
    }


    public get items(): SubThingyViewResult[] {
        var embeds = this.client.GetEmbed("values");
        var clients = embeds.GetAllClients();
        var result: SubThingyViewResult[] = [];
        for (var i = 0; i < clients.length; ++i) {
            result.push(new SubThingyViewResult(clients[i]));
        }
        return result;
    }


    public refresh(): Promise<SubThingyCollectionViewResult> {
        return this.client.LoadLink("self")
            .then(r => {
                return new SubThingyCollectionViewResult(r);
            });
    }

    public canRefresh(): boolean {
        return this.client.HasLink("self");
    }


    public listSubThingies(): Promise<SubThingyCollectionViewResult> {
        return this.client.LoadLink("listSubThingies")
            .then(r => {
                return new SubThingyCollectionViewResult(r);
            });
    }

    public canListSubThingies(): boolean {
        return this.client.HasLink("listSubThingies");
    }


    public next() {
        return this.client.LoadLink("next")
            .then(r => {
                return r;
            });
    }

    public canNext(): boolean {
        return this.client.HasLink("next");
    }


    public previous() {
        return this.client.LoadLink("previous")
            .then(r => {
                return r;
            });
    }

    public canPrevious(): boolean {
        return this.client.HasLink("previous");
    }


    public first() {
        return this.client.LoadLink("first")
            .then(r => {
                return r;
            });
    }

    public canFirst(): boolean {
        return this.client.HasLink("first");
    }


    public last() {
        return this.client.LoadLink("last")
            .then(r => {
                return r;
            });
    }

    public canLast(): boolean {
        return this.client.HasLink("last");
    }

}

export class SubThingyViewResult {
    private client: hal.HalEndpointClient;

    constructor(client: hal.HalEndpointClient) {
        this.client = client;
    }

    public get data(): SubThingyView {
        return this.client.GetData<SubThingyView>();
    }


    public refresh(): Promise<SubThingyViewResult> {
        return this.client.LoadLink("self")
            .then(r => {
                return new SubThingyViewResult(r);
            });
    }

    public canRefresh(): boolean {
        return this.client.HasLink("self");
    }


    public listSubThingies(): Promise<SubThingyCollectionViewResult> {
        return this.client.LoadLink("listSubThingies")
            .then(r => {
                return new SubThingyCollectionViewResult(r);
            });
    }

    public canListSubThingies(): boolean {
        return this.client.HasLink("listSubThingies");
    }


    public updateSubThingy(data: SubThingyView): Promise<SubThingyViewResult> {
        return this.client.LoadLinkWithBody("updateSubThingy", data)
            .then(r => {
                return new SubThingyViewResult(r);
            });
    }

    public canUpdateSubThingy(): boolean {
        return this.client.HasLink("updateSubThingy");
    }


    public deleteSubThingy() {
        return this.client.LoadLink("deleteSubThingy")
            .then(r => {
                return r;
            });
    }

    public canDeleteSubThingy(): boolean {
        return this.client.HasLink("deleteSubThingy");
    }


    public getSubThingy(): Promise<SubThingyViewResult> {
        return this.client.LoadLink("getSubThingy")
            .then(r => {
                return new SubThingyViewResult(r);
            });
    }

    public canGetSubThingy(): boolean {
        return this.client.HasLink("getSubThingy");
    }


    public getThingy(): Promise<ThingyViewResult> {
        return this.client.LoadLink("getThingy")
            .then(r => {
                return new ThingyViewResult(r);
            });
    }

    public canGetThingy(): boolean {
        return this.client.HasLink("getThingy");
    }

}

export class ThingyCollectionViewResult {
    private client: hal.HalEndpointClient;

    constructor(client: hal.HalEndpointClient) {
        this.client = client;
    }

    public get data(): ThingyCollectionView {
        return this.client.GetData<ThingyCollectionView>();
    }


    public get items(): ThingyViewResult[] {
        var embeds = this.client.GetEmbed("values");
        var clients = embeds.GetAllClients();
        var result: ThingyViewResult[] = [];
        for (var i = 0; i < clients.length; ++i) {
            result.push(new ThingyViewResult(clients[i]));
        }
        return result;
    }


    public refresh(): Promise<ThingyCollectionViewResult> {
        return this.client.LoadLink("self")
            .then(r => {
                return new ThingyCollectionViewResult(r);
            });
    }

    public canRefresh(): boolean {
        return this.client.HasLink("self");
    }


    public listThingies(query: CollectionQuery): Promise<ThingyCollectionViewResult> {
        return this.client.LoadLinkWithQuery("listThingies", query)
            .then(r => {
                return new ThingyCollectionViewResult(r);
            });
    }

    public canListThingies(): boolean {
        return this.client.HasLink("listThingies");
    }


    public addThingy(data: ThingyView): Promise<ThingyViewResult> {
        return this.client.LoadLinkWithBody("addThingy", data)
            .then(r => {
                return new ThingyViewResult(r);
            });
    }

    public canAddThingy(): boolean {
        return this.client.HasLink("addThingy");
    }


    public next() {
        return this.client.LoadLink("next")
            .then(r => {
                return r;
            });
    }

    public canNext(): boolean {
        return this.client.HasLink("next");
    }


    public previous() {
        return this.client.LoadLink("previous")
            .then(r => {
                return r;
            });
    }

    public canPrevious(): boolean {
        return this.client.HasLink("previous");
    }


    public first() {
        return this.client.LoadLink("first")
            .then(r => {
                return r;
            });
    }

    public canFirst(): boolean {
        return this.client.HasLink("first");
    }


    public last() {
        return this.client.LoadLink("last")
            .then(r => {
                return r;
            });
    }

    public canLast(): boolean {
        return this.client.HasLink("last");
    }

}

export class ThingyViewResult {
    private client: hal.HalEndpointClient;

    constructor(client: hal.HalEndpointClient) {
        this.client = client;
    }

    public get data(): ThingyView {
        return this.client.GetData<ThingyView>();
    }


    public refresh(): Promise<ThingyViewResult> {
        return this.client.LoadLink("self")
            .then(r => {
                return new ThingyViewResult(r);
            });
    }

    public canRefresh(): boolean {
        return this.client.HasLink("self");
    }


    public getThingy(): Promise<ThingyViewResult> {
        return this.client.LoadLink("getThingy")
            .then(r => {
                return new ThingyViewResult(r);
            });
    }

    public canGetThingy(): boolean {
        return this.client.HasLink("getThingy");
    }


    public updateThingy(data: ThingyView): Promise<ThingyViewResult> {
        return this.client.LoadLinkWithBody("updateThingy", data)
            .then(r => {
                return new ThingyViewResult(r);
            });
    }

    public canUpdateThingy(): boolean {
        return this.client.HasLink("updateThingy");
    }


    public deleteThingy() {
        return this.client.LoadLink("deleteThingy")
            .then(r => {
                return r;
            });
    }

    public canDeleteThingy(): boolean {
        return this.client.HasLink("deleteThingy");
    }


    public listThingySubThingies(): Promise<SubThingyCollectionViewResult> {
        return this.client.LoadLink("listThingySubThingies")
            .then(r => {
                return new SubThingyCollectionViewResult(r);
            });
    }

    public canListThingySubThingies(): boolean {
        return this.client.HasLink("listThingySubThingies");
    }


    public addSubThingy(data: SubThingyView): Promise<SubThingyViewResult> {
        return this.client.LoadLinkWithBody("addSubThingy", data)
            .then(r => {
                return new SubThingyViewResult(r);
            });
    }

    public canAddSubThingy(): boolean {
        return this.client.HasLink("addSubThingy");
    }


    public authorizedpropertiesThingies() {
        return this.client.LoadLink("authorizedpropertiesThingies")
            .then(r => {
                return r;
            });
    }

    public canAuthorizedpropertiesThingies(): boolean {
        return this.client.HasLink("authorizedpropertiesThingies");
    }


    public rolepropertiesThingies() {
        return this.client.LoadLink("rolepropertiesThingies")
            .then(r => {
                return r;
            });
    }

    public canRolepropertiesThingies(): boolean {
        return this.client.HasLink("rolepropertiesThingies");
    }


    public testDeclareLinkToRel(query: CollectionQuery) {
        return this.client.LoadLinkWithQuery("testDeclareLinkToRel", query)
            .then(r => {
                return r;
            });
    }

    public canTestDeclareLinkToRel(): boolean {
        return this.client.HasLink("testDeclareLinkToRel");
    }

}
export interface EntryPoints {
}
/** Default implementation of ICollectionQuery. */
export interface CollectionQuery {
    /** The number of pages (item number = Offset * Limit) into the collection to query. */
    offset?: number;
    /** The limit of the number of items to return. */
    limit?: number;
}
export interface ThingyCollectionView {
    offset?: number;
    limit?: number;
    total?: number;
}
export interface SubThingyCollectionView {
    offset?: number;
    limit?: number;
    total?: number;
}
export interface SubThingyView {
    subThingyId?: number;
    amount?: number;
    thingyId?: number;
}
/** A simple test model. */
export interface ThingyView {
    /** Id, it is important to fully name it here and in routes to avoid naming collisions. */
    thingyId?: number;
    /** Name, provides some test data. */
    name: string;
}

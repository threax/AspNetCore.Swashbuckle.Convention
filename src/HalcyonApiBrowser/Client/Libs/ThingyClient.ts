import * as hal from 'hr.halcyon.EndpointClient';
import { Fetcher } from 'hr.fetcher';

export class EntryPointsResultView {
    private client: hal.HalEndpointClient;

    constructor(client: hal.HalEndpointClient) {
        this.client = client;
    }

    public get data(): EntryPoints {
        return this.client.GetData<EntryPoints>();
    }


    public refresh(): Promise<EntryPointsResultView> {
        return this.client.LoadLink("self")
            .then(r => {
                return new EntryPointsResultView(r);
            });
    }

    public canrefresh(): boolean {
        return this.client.HasLink("self");
    }


    public listThingies(query: CollectionQuery): Promise<ThingyCollectionViewResultView> {
        return this.client.LoadLinkWithQuery("listThingies", query)
            .then(r => {
                return new ThingyCollectionViewResultView(r);
            });
    }

    public canlistThingies(): boolean {
        return this.client.HasLink("listThingies");
    }


}

export class SubThingyCollectionViewResultView {
    private client: hal.HalEndpointClient;

    constructor(client: hal.HalEndpointClient) {
        this.client = client;
    }

    public get data(): SubThingyCollectionView {
        return this.client.GetData<SubThingyCollectionView>();
    }


    public refresh(): Promise<SubThingyCollectionViewResultView> {
        return this.client.LoadLink("self")
            .then(r => {
                return new SubThingyCollectionViewResultView(r);
            });
    }

    public canrefresh(): boolean {
        return this.client.HasLink("self");
    }


    public listSubThingies(): Promise<SubThingyCollectionViewResultView> {
        return this.client.LoadLink("listSubThingies")
            .then(r => {
                return new SubThingyCollectionViewResultView(r);
            });
    }

    public canlistSubThingies(): boolean {
        return this.client.HasLink("listSubThingies");
    }


}

export class SubThingyViewResultView {
    private client: hal.HalEndpointClient;

    constructor(client: hal.HalEndpointClient) {
        this.client = client;
    }

    public get data(): SubThingyView {
        return this.client.GetData<SubThingyView>();
    }


    public refresh(): Promise<SubThingyViewResultView> {
        return this.client.LoadLink("self")
            .then(r => {
                return new SubThingyViewResultView(r);
            });
    }

    public canrefresh(): boolean {
        return this.client.HasLink("self");
    }


    public listSubThingies(): Promise<SubThingyCollectionViewResultView> {
        return this.client.LoadLink("listSubThingies")
            .then(r => {
                return new SubThingyCollectionViewResultView(r);
            });
    }

    public canlistSubThingies(): boolean {
        return this.client.HasLink("listSubThingies");
    }


    public updateSubThingy(data: SubThingyView): Promise<SubThingyViewResultView> {
        return this.client.LoadLinkWithBody("updateSubThingy", data)
            .then(r => {
                return new SubThingyViewResultView(r);
            });
    }

    public canupdateSubThingy(): boolean {
        return this.client.HasLink("updateSubThingy");
    }


    public deleteSubThingy() {
        return this.client.LoadLink("deleteSubThingy")
            .then(r => {
                return r;
            });
    }

    public candeleteSubThingy(): boolean {
        return this.client.HasLink("deleteSubThingy");
    }


    public getSubThingy(): Promise<SubThingyViewResultView> {
        return this.client.LoadLink("getSubThingy")
            .then(r => {
                return new SubThingyViewResultView(r);
            });
    }

    public cangetSubThingy(): boolean {
        return this.client.HasLink("getSubThingy");
    }


    public getThingy(): Promise<ThingyViewResultView> {
        return this.client.LoadLink("getThingy")
            .then(r => {
                return new ThingyViewResultView(r);
            });
    }

    public cangetThingy(): boolean {
        return this.client.HasLink("getThingy");
    }


}

export class ThingyCollectionViewResultView {
    private client: hal.HalEndpointClient;

    constructor(client: hal.HalEndpointClient) {
        this.client = client;
    }

    public get data(): ThingyCollectionView {
        return this.client.GetData<ThingyCollectionView>();
    }


    public refresh(): Promise<ThingyCollectionViewResultView> {
        return this.client.LoadLink("self")
            .then(r => {
                return new ThingyCollectionViewResultView(r);
            });
    }

    public canrefresh(): boolean {
        return this.client.HasLink("self");
    }


    public listThingies(query: CollectionQuery): Promise<ThingyCollectionViewResultView> {
        return this.client.LoadLinkWithQuery("listThingies", query)
            .then(r => {
                return new ThingyCollectionViewResultView(r);
            });
    }

    public canlistThingies(): boolean {
        return this.client.HasLink("listThingies");
    }


    public addThingy(data: ThingyView): Promise<ThingyViewResultView> {
        return this.client.LoadLinkWithBody("addThingy", data)
            .then(r => {
                return new ThingyViewResultView(r);
            });
    }

    public canaddThingy(): boolean {
        return this.client.HasLink("addThingy");
    }


}

export class ThingyViewResultView {
    private client: hal.HalEndpointClient;

    constructor(client: hal.HalEndpointClient) {
        this.client = client;
    }

    public get data(): ThingyView {
        return this.client.GetData<ThingyView>();
    }


    public refresh(): Promise<ThingyViewResultView> {
        return this.client.LoadLink("self")
            .then(r => {
                return new ThingyViewResultView(r);
            });
    }

    public canrefresh(): boolean {
        return this.client.HasLink("self");
    }


    public getThingy(): Promise<ThingyViewResultView> {
        return this.client.LoadLink("getThingy")
            .then(r => {
                return new ThingyViewResultView(r);
            });
    }

    public cangetThingy(): boolean {
        return this.client.HasLink("getThingy");
    }


    public updateThingy(data: ThingyView): Promise<ThingyViewResultView> {
        return this.client.LoadLinkWithBody("updateThingy", data)
            .then(r => {
                return new ThingyViewResultView(r);
            });
    }

    public canupdateThingy(): boolean {
        return this.client.HasLink("updateThingy");
    }


    public deleteThingy() {
        return this.client.LoadLink("deleteThingy")
            .then(r => {
                return r;
            });
    }

    public candeleteThingy(): boolean {
        return this.client.HasLink("deleteThingy");
    }


    public listThingySubThingies(): Promise<SubThingyCollectionViewResultView> {
        return this.client.LoadLink("listThingySubThingies")
            .then(r => {
                return new SubThingyCollectionViewResultView(r);
            });
    }

    public canlistThingySubThingies(): boolean {
        return this.client.HasLink("listThingySubThingies");
    }


    public addSubThingy(data: SubThingyView): Promise<SubThingyViewResultView> {
        return this.client.LoadLinkWithBody("addSubThingy", data)
            .then(r => {
                return new SubThingyViewResultView(r);
            });
    }

    public canaddSubThingy(): boolean {
        return this.client.HasLink("addSubThingy");
    }


    public authorizedpropertiesThingies() {
        return this.client.LoadLink("authorizedpropertiesThingies")
            .then(r => {
                return r;
            });
    }

    public canauthorizedpropertiesThingies(): boolean {
        return this.client.HasLink("authorizedpropertiesThingies");
    }


    public rolepropertiesThingies() {
        return this.client.LoadLink("rolepropertiesThingies")
            .then(r => {
                return r;
            });
    }

    public canrolepropertiesThingies(): boolean {
        return this.client.HasLink("rolepropertiesThingies");
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

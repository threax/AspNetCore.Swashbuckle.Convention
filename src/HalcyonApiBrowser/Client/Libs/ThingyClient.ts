import * as hal from 'hr.halcyon.EndpointClient';
import { Fetcher } from 'hr.fetcher';

export class EntryPointsResult {
    private client: hal.HalEndpointClient;

    public static Load(url: string, fetcher: Fetcher): Promise<EntryPointsResult> {
        return hal.HalEndpointClient.Load({
            href: url,
            method: "GET"
        }, fetcher)
            .then(c => {
                return new EntryPointsResult(c);
            });
    }

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

    public getRefreshDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("self")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasRefreshDocs(): boolean {
        return this.client.HasLinkDoc("self");
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

    public getListThingiesDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("listThingies")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasListThingiesDocs(): boolean {
        return this.client.HasLinkDoc("listThingies");
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

    public getRefreshDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("self")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasRefreshDocs(): boolean {
        return this.client.HasLinkDoc("self");
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

    public getListSubThingiesDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("listSubThingies")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasListSubThingiesDocs(): boolean {
        return this.client.HasLinkDoc("listSubThingies");
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

    public getRefreshDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("self")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasRefreshDocs(): boolean {
        return this.client.HasLinkDoc("self");
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

    public getListSubThingiesDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("listSubThingies")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasListSubThingiesDocs(): boolean {
        return this.client.HasLinkDoc("listSubThingies");
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

    public getUpdateSubThingyDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("updateSubThingy")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasUpdateSubThingyDocs(): boolean {
        return this.client.HasLinkDoc("updateSubThingy");
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

    public getGetSubThingyDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("getSubThingy")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasGetSubThingyDocs(): boolean {
        return this.client.HasLinkDoc("getSubThingy");
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

    public getGetThingyDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("getThingy")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasGetThingyDocs(): boolean {
        return this.client.HasLinkDoc("getThingy");
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

    public getRefreshDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("self")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasRefreshDocs(): boolean {
        return this.client.HasLinkDoc("self");
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

    public getListThingiesDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("listThingies")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasListThingiesDocs(): boolean {
        return this.client.HasLinkDoc("listThingies");
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

    public getAddThingyDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("addThingy")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasAddThingyDocs(): boolean {
        return this.client.HasLinkDoc("addThingy");
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

    public getRefreshDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("self")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasRefreshDocs(): boolean {
        return this.client.HasLinkDoc("self");
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

    public getGetThingyDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("getThingy")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasGetThingyDocs(): boolean {
        return this.client.HasLinkDoc("getThingy");
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

    public getUpdateThingyDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("updateThingy")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasUpdateThingyDocs(): boolean {
        return this.client.HasLinkDoc("updateThingy");
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

    public getListThingySubThingiesDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("listThingySubThingies")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasListThingySubThingiesDocs(): boolean {
        return this.client.HasLinkDoc("listThingySubThingies");
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

    public getAddSubThingyDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("addSubThingy")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasAddSubThingyDocs(): boolean {
        return this.client.HasLinkDoc("addSubThingy");
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

    public getTestDeclareLinkToRelDocs(): Promise<hal.HalEndpointDoc> {
        return this.client.LoadLinkDoc("testDeclareLinkToRel")
            .then(r => {
                return r.GetData<hal.HalEndpointDoc>();
            });
    }

    public hasTestDeclareLinkToRelDocs(): boolean {
        return this.client.HasLinkDoc("testDeclareLinkToRel");
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


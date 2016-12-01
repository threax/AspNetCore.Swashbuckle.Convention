import { Fetcher } from 'hr.fetcher';

/**
 * This interface strongly types the hal endpoint data.
 * @param {any} links
 */
interface HalData {
    _links: any,
    _embedded: any
}

/**
 * A single hal link how they appear in the collection.
 * @param {any} embeds
 */
export interface HalLink {
    href: string,
    method: string
}

/**
 * Info about a single hal link, will include the link's ref.
 * @param {any} embeds
 */
export interface HalLinkInfo {
    href: string,
    method: string,
    rel: string
}

export class Embed<T>{
    private name: string;
    private embeds: HalData[];
    private fetcher: Fetcher;

    constructor(name: string, embeds: HalData[], fetcher: Fetcher) {
        this.name = name;
        this.embeds = embeds;
        this.fetcher = fetcher;
    }

    public GetAllClients(): HalEndpointClient<T>[] {
        //No generators, create array
        var embeddedClients: HalEndpointClient<T>[] = [];

        for (let i = 0; i < this.embeds.length; ++i) {
            var embed = new HalEndpointClient<T>(this.embeds[i], this.fetcher);
            embeddedClients.push(embed);
        }
        return embeddedClients;
    }
}

/**
 * This class represents a single visit to a hal api endpoint. It will contain the data
 * that was requested and the links from that data. The hal properties are removed
 * from the data, so if you get it it won't contain that info.
 */
export class HalEndpointClient<T> {
    private static jsonMimeType = "application/json";

    /**
     * Load a hal link from an endpoint.
     * @param {HalLink} link - The link to load
     * @param {Fetcher} fetcher - The fetcher to use to load the link
     * @returns A HalEndpointClient for the link.
     */
    public static Load<T>(link: HalLink, fetcher: Fetcher, reqBody?: any): Promise<HalEndpointClient<T>> {
        var body;
        if (reqBody !== undefined) {
            body = JSON.stringify(reqBody);
        }
        return fetcher.fetch(link.href, {
            method: link.method,
            body: body,
            headers: {
                "Content-Type": "application/json; charset=UTF-8"
            }
        })
        .then(r => HalEndpointClient.processResult<T>(r, fetcher));
    }

    private static processResult<T>(response: Response, fetcher: Fetcher): Promise<HalEndpointClient<T>> {
        return response.text().then((data) => {
            if (response.ok) {
                return new HalEndpointClient<T>(HalEndpointClient.parseResult(response, data), fetcher);
            }
            else {
                throw new Error("Error Code " + response.status + " Returned. Make this error work better.");
            }
        });
    }

    private static parseResult(response: Response, data: string, jsonParseReviver?: (key: string, value: any) => any): HalData {
        var result: HalData;
        var contentHeader = response.headers.get('content-type');
        if (contentHeader && contentHeader.length >= HalEndpointClient.jsonMimeType.length && contentHeader.substring(0, HalEndpointClient.jsonMimeType.length) === HalEndpointClient.jsonMimeType) {
            result = data === "" ? null : JSON.parse(data, jsonParseReviver);
        }
        else {
            throw new Error("Unsupported response type " + contentHeader + ".");
        }
        return result;
    }

    private data: T; //This is our typed data the hal properties are removed
    private fetcher: Fetcher;
    private embeds;
    private links;

    /**
     * Constructor.
     * @param {HalData} data - The raw hal data object.
     */
    constructor(data: HalData, fetcher: Fetcher) {
        this.embeds = data._embedded;
        delete data._embedded;
        this.links = data._links;
        delete data._links;
        this.data = <any>data; //HalData is the actual data, trick compiler
        this.fetcher = fetcher;
    }

    /**
     * Get the data portion of this client.
     * @returns The data.
     */
    public GetData(): T {
        return this.data;
    }

    /**
     * Get an embed.
     * @param {string} name - The name of the embed.
     * @returns - The embed specified by name or undefined.
     */
    public GetEmbed<T>(name: string): Embed<T> {
        return new Embed<T>(name, this.embeds[name], this.fetcher);
    }

    /**
     * See if this client has an embed.
     * @param {string} name - The name of the embed
     * @returns True if found, false otherwise.
     */
    public HasEmbed(name: string): boolean {
        return this.embeds[name] !== undefined;
    }

    /**
     * Get all the embeds in this client. If they are all the same type specify
     * T, otherwise use any to get generic objects.
     * @returns
     */
    public GetAllEmbeds<T>(): Embed<T>[] {
        //No generators, create array
        var embeds: Embed<T>[] = [];
        for (var key in this.embeds) {
            var embed = new Embed<T>(key, this.embeds[key], this.fetcher);
            embeds.push(embed);
        }
        return embeds;
    }

    /**
     * Load a new link, this will return a new HalEndpointClient for the results
     * of that request. You can keep using the client that you called this function
     * on to keep making requests if needed. The ref must exist before you can call
     * this function. Use HasLink to see if it is possible.
     * @param {string} ref - The link reference to visit.
     * @returns
     */
    public LoadLink<ResultType>(ref: string): Promise<HalEndpointClient<ResultType>> {
        if (this.HasLink(ref)) {
            return HalEndpointClient.Load<ResultType>(this.GetLink(ref), this.fetcher);
        }
        else {
            throw new Error('Cannot find ref "' + ref + '".');
        }
    }

    /**
     * Load a new link, this will return a new HalEndpointClient for the results
     * of that request. You can keep using the client that you called this function
     * on to keep making requests if needed. The ref must exist before you can call
     * this function. Use HasLink to see if it is possible.
     * @param {string} ref - The link reference to visit.
     * @param {string} data - The data to send as the body of the request
     * @returns
     */
    public LoadLinkWith<ResultType, RequestType>(ref: string, data: RequestType): Promise<HalEndpointClient<ResultType>> {
        if (this.HasLink(ref)) {
            return HalEndpointClient.Load<ResultType>(this.GetLink(ref), this.fetcher, data);
        }
        else {
            throw new Error('Cannot find ref "' + ref + '".');
        }
    }

    /**
     * Get a single named link.
     * @param {string} ref - The name of the link to recover.
     * @returns The link or undefined if the link does not exist.
     */
    public GetLink(ref: string): HalLink {
        return this.links[ref];
    }

    /**
     * Check to see if a link exists in this collection.
     * @param {string} ref - The name of the link (the ref).
     * @returns - True if the link exists, false otherwise
     */
    public HasLink(ref: string): boolean {
        return this.links[ref] !== undefined;
    }

    /**
     * Get all links in this collection. Will transform them to a HalLinkInfo, these are copies of the original links with ref added.
     * @returns
     */
    public GetAllLinks(): HalLinkInfo[] {
        //If only we had generators, have to load entire collection
        var linkInfos: HalLinkInfo[] = [];
        for (var key in this.links) {
            var link: HalLink = this.links[key];
            linkInfos.push({
                href: link.href,
                method: link.method,
                rel: key
            });
        }
        return linkInfos;
    }
}
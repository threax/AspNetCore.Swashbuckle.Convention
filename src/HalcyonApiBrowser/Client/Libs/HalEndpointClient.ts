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
 * This class provides info for hal links.
 * @param {any} links The links to wrap.
 * @returns
 */
export class HalLinks {
    private links: any;

    constructor(links: any) {
        this.links = links;
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

/**
 * This class represents embeds in a hal object.
 * @param {any} embeds
 * @returns
 */
export class HalEmbeds {
    private embeds: any;

    constructor(embeds: any) {
        this.embeds = embeds;
    }

    public GetEmbed<T>(name: string): HalEndpointClient<T> {
        return new HalEndpointClient<T>(<HalData>this.embeds[name]);
    }

    public HasEmbed(name: string): boolean {
        return this.embeds[name] !== undefined;
    }
}

/**
 * This class represents a single visit to a hal api endpoint. It will contain the data
 * that was requested and the links from that data.
 */
export class HalEndpointClient<T> {
    private static jsonMimeType = "application/json";

    /**
     * Load a hal link from an endpoint.
     * @param {HalLink} link - The link to load
     * @param {Fetcher} fetcher - The fetcher to use to load the link
     * @returns A HalEndpointClient for the link.
     */
    public static Load<T>(link: HalLink, fetcher: Fetcher): Promise<HalEndpointClient<T>> {
        return fetcher.fetch(link.href, {
            method: link.method
        })
        .then(r => HalEndpointClient.processResult<T>(r));
    }

    private static processResult<T>(response: Response): Promise<HalEndpointClient<T>> {
        return response.text().then((data) => {
            if (response.ok) {
                return new HalEndpointClient<T>(HalEndpointClient.parseResult(response, data));
            }
            else {
                throw new Error("Error Code Returned. Make this error work better.");
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

    private data: HalData; //This is our typed data, but as a hal object

    /**
     * Constructor.
     * @param {HalData} data - The raw hal data object.
     */
    constructor(data: HalData) {
        this.data = data;
    }

    /**
     * Get the data portion of this client.
     * @returns The data.
     */
    public GetData(): T {
        //Tricky typecase, but HalDatas are also Ts in this instance.
        //Not here or relevant at runtime anyway.
        return <T><any>this.data;
    }

    /**
     * Get the hal links returned in the data.
     * @returns The link collection.
     */
    public GetLinks(): HalLinks {
        return new HalLinks(this.data._links);
    }

    /**
     * Get the embeds returned in the data.
     * @returns The embedded data collection.
     */
    public GetEmbeds(): HalEmbeds{
        return new HalEmbeds(this.data._embedded);
    }
}
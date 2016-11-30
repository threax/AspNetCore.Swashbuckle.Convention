import { Fetcher } from 'hr.fetcher';

interface HalData {
    _links: any,
    _embedded: any
}

export class HalLinks {
    private links: any;

    constructor(links: any) {
        this.links = links;
    }

    public GetLink(name: string): HalLink {
        return this.links[name];
    }

    public HasLink(name: string): boolean {
        return this.links[name] !== undefined;
    }
}

export interface HalLink {
    href: string,
    method: string
}

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

export class HalEndpointClient<T> {
    private static jsonMimeType = "application/json";

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

    constructor(data: HalData) {
        this.data = data;
    }

    public GetData(): T {
        //Tricky typecase, but HalDatas are also Ts in this instance.
        //Not here or relevant at runtime anyway.
        return <T><any>this.data;
    }

    public GetLinks(): HalLinks {
        return new HalLinks(this.data._links);
    }

    public GetEmbeds(): HalEmbeds{
        return new HalEmbeds(this.data._embedded);
    }
}
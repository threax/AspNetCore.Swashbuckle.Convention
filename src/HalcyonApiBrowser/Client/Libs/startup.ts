import * as controller from 'hr.controller';
import * as WindowFetch from 'hr.windowfetch';
import * as AccessTokens from 'hr.accesstokens';
import * as fetcher from 'hr.fetcher';
import * as bootstrap from 'hr.bootstrap.all';

interface Query {
    entry: string;
}

export function addServices(services: controller.ServiceCollection) {
    //Keep this bootstrap activator line, it will ensure that bootstrap is loaded and configured before continuing.
    bootstrap.activate();

    //Set up the access token fetcher
    var config = <Query>(<any>window).clientConfig;
    if (!config) {
        config = {
            entry: ""
        };
    }

    services.tryAddShared(fetcher.Fetcher, s => new WindowFetch.WindowFetch());
}
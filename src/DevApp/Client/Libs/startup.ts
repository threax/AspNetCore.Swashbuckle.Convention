import * as controller from 'hr.controller';
import * as WindowFetch from 'hr.windowfetch';
import * as whitelist from 'hr.whitelist';
import * as AccessTokens from 'hr.accesstokens';
import * as fetcher from 'hr.fetcher';
import * as bootstrap from 'hr.bootstrap.all';
import * as client from 'clientlibs.ServiceClient';
import * as xsrf from 'hr.xsrftoken';
//import * as loginPopup from 'threax.theme.LoginPopup';
import * as deepLink from 'hr.deeplink';

export interface ClientConfig {
    ServiceUrl: string;
    UserDirectoryUrl: string;
    PageBasePath: string;
}

export interface TokenConfig {
    AccessTokenPath?: string;
    XsrfCookie?: string;
    XsrfPaths?: string[];
}

var builder: controller.InjectedControllerBuilder = null;

export function createBuilder() {
    if (builder === null) {
        builder = new controller.InjectedControllerBuilder();

        //Keep this bootstrap activator line, it will ensure that bootstrap is loaded and configured before continuing.
        bootstrap.activate();

        //Set up the access token fetcher
        var config = <ClientConfig>(<any>window).clientConfig;
        builder.Services.tryAddShared(fetcher.Fetcher, s => createFetcher(config, <TokenConfig>(<any>window).xsrfConfig));

        builder.Services.tryAddShared(client.EntryPointInjector, s => new client.EntryPointInjector(config.ServiceUrl, s.getRequiredService(fetcher.Fetcher)));
        //Map the role entry point to the service entry point and add the user directory
        //builder.Services.addShared(roleClient.IRoleEntryInjector, s => s.getRequiredService(client.EntryPointInjector));
        //builder.Services.addShared(UserDirectoryEntryPointInjector, s => new UserDirectoryEntryPointInjector(config.UserDirectoryUrl, s.getRequiredService(fetcher.Fetcher)));

        //Setup Deep Links
        deepLink.setPageUrl(builder.Services, config.PageBasePath);

        //Setup relogin
        //loginPopup.addServices(builder.Services);
        //builder.create("threax-relogin", loginPopup.LoginPopup);
    }
    return builder;
}

function createFetcher(config: ClientConfig, tokenConfig: TokenConfig): fetcher.Fetcher {
    var fetcher = new WindowFetch.WindowFetch();

    if (tokenConfig !== undefined) {
        fetcher = new xsrf.XsrfTokenFetcher(
            new xsrf.CookieTokenAccessor(tokenConfig.XsrfCookie),
            new whitelist.Whitelist(tokenConfig.XsrfPaths),
            fetcher);
    }

    if (tokenConfig.AccessTokenPath !== undefined) {
        fetcher = new AccessTokens.AccessTokenFetcher(
            tokenConfig.AccessTokenPath,
            //Below would also need , config.UserDirectoryUrl
            new whitelist.Whitelist([config.ServiceUrl]),
            fetcher);
    }

    return fetcher;
}
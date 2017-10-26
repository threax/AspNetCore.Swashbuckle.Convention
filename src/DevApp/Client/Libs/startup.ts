import * as controller from 'hr.controller';
import * as WindowFetch from 'hr.windowfetch';
import * as AccessTokens from 'hr.accesstokens';
import * as fetcher from 'hr.fetcher';
import * as bootstrap from 'hr.bootstrap.all';
import * as client from 'clientlibs.ServiceClient';
//import * as loginPopup from 'threax.theme.LoginPopup';
import * as deepLink from 'hr.deeplink';

export interface ClientConfig {
    ServiceUrl: string,
    AccessTokenPath: string,
    UserDirectoryUrl: string,
    PageBasePath: string
}

var builder: controller.InjectedControllerBuilder = null;

export function createBuilder() {
    if (builder === null) {
        builder = new controller.InjectedControllerBuilder();

        //Keep this bootstrap activator line, it will ensure that bootstrap is loaded and configured before continuing.
        bootstrap.activate();

        //Set up the access token fetcher
        var config = <ClientConfig>(<any>window).clientConfig;
        //Below would also need , config.UserDirectoryUrl
        var bearerTokenWhitelist = new AccessTokens.AccessWhitelist([config.ServiceUrl]); //Default whitelist adds the service and user directory urls, if you add more add them to this array
        builder.Services.tryAddShared(fetcher.Fetcher, s =>
            new AccessTokens.AccessTokenManager(config.AccessTokenPath, bearerTokenWhitelist,
                new WindowFetch.WindowFetch()));
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
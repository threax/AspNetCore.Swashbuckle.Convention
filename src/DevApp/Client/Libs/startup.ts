﻿import * as controller from 'hr.controller';
import * as WindowFetch from 'hr.windowfetch';
import * as whitelist from 'hr.whitelist';
import * as AccessTokens from 'hr.accesstokens';
import * as fetcher from 'hr.fetcher';
import * as bootstrap from 'hr.bootstrap.all';
import * as client from 'clientlibs.ServiceClient';
import * as xsrf from 'hr.xsrftoken';
import * as loginPopup from 'hr.relogin.LoginPopup';
import * as deepLink from 'hr.deeplink';
import * as pageConfig from 'hr.pageconfig';
import * as roleClient from 'hr.roleclient.RoleClient';
import { EntryPointInjector as UserDirectoryEntryPointInjector } from 'hr.roleclient.UserDirectoryClient';

export interface Config {
    client: {
        ServiceUrl: string;
        UserDirectoryUrl: string;
        PageBasePath: string;
    };
    tokens?: {
        AccessTokenPath?: string;
        XsrfCookie?: string;
        XsrfPaths?: string[];
    }
}

var builder: controller.InjectedControllerBuilder = null;

export function createBuilder() {
    if (builder === null) {
        builder = new controller.InjectedControllerBuilder();

        //Keep this bootstrap activator line, it will ensure that bootstrap is loaded and configured before continuing.
        bootstrap.activate();

        //Set up the access token fetcher
        var config = pageConfig.read<Config>();
        builder.Services.tryAddShared(fetcher.Fetcher, s => createFetcher(config));

        builder.Services.tryAddShared(client.EntryPointInjector, s => new client.EntryPointInjector(config.client.ServiceUrl, s.getRequiredService(fetcher.Fetcher)));
        //Map the role entry point to the service entry point and add the user directory
        builder.Services.addShared(roleClient.IRoleEntryInjector, s => s.getRequiredService(client.EntryPointInjector));
        builder.Services.addShared(UserDirectoryEntryPointInjector, s => new UserDirectoryEntryPointInjector(config.client.UserDirectoryUrl, s.getRequiredService(fetcher.Fetcher)));

        //Setup Deep Links
        deepLink.setPageUrl(builder.Services, config.client.PageBasePath);

        //Setup relogin
        loginPopup.addServices(builder.Services);
        builder.create("hr-relogin", loginPopup.LoginPopup);
    }
    return builder;
}

function createFetcher(config: Config): fetcher.Fetcher {
    var fetcher = new WindowFetch.WindowFetch();

    if (config.tokens !== undefined) {
        fetcher = new xsrf.XsrfTokenFetcher(
            new xsrf.CookieTokenAccessor(config.tokens.XsrfCookie),
            new whitelist.Whitelist(config.tokens.XsrfPaths),
            fetcher);
    }

    if (config.tokens.AccessTokenPath !== undefined) {
        fetcher = new AccessTokens.AccessTokenFetcher(
            config.tokens.AccessTokenPath,
            new whitelist.Whitelist([config.client.ServiceUrl, config.client.UserDirectoryUrl]),
            fetcher);
    }

    return fetcher;
}
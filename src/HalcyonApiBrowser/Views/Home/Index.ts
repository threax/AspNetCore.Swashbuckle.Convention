import * as PageStart from 'clientlibs.PageStart';
import { HalcyonBrowserController } from 'clientlibs.HalcyonBrowserController';

PageStart.init()
    .then(config => {
        var browsers = HalcyonBrowserController.Builder().create("halcyonbrowser");
        browsers[0].showResults(config.EntryPoint);
    });
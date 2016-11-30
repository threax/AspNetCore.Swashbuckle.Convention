import * as PageStart from 'clientlibs.PageStart';
import { HalcyonBrowserController } from 'clientlibs.HalcyonBrowserController';

PageStart.init()
    .then(config => {
        HalcyonBrowserController.Builder(config).create("halcyonbrowser");
    });
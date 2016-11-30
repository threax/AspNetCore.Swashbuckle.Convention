import * as bootstrap from 'hr.bootstrap.all';

export class PageStart {
    constructor() {
        //Activate bootstrap here, this way we know its active and has registered all its models and toggles when starting up other controllers.
        bootstrap.activate();
    }
}

var instance: PageStart = null;
/**
 * Set up common config for the page to run.
 * @returns A Promise with the PageStart instance inside.
 */
export function init(): Promise<PageStart> {
    if (instance === null) {
        instance = new PageStart();
    }

    return Promise.resolve(instance);
}
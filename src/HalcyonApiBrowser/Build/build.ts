import * as copy from 'threax-npm-tk/copy';
import * as less from 'threax-npm-tk/less';
import { tsc } from 'threax-npm-tk/typescript';
import * as jsnsTools from 'threax-npm-tk/jsnstools';
import * as artifact from 'threax-npm-tk/artifacts';

var filesDir = __dirname + "/..";

build(filesDir + "/wwwroot", filesDir + "/wwwroot", filesDir + "/node_modules");

export function build(outDir, iconOutPath, moduleDir): Promise<any> {
    var promises = [];

    //Build bootstrap theme
    promises.push(less.compile({
        encoding: 'utf8',
        importPaths: [moduleDir, moduleDir + '/bootstrap/less'],
        input: filesDir + '/bootstrap/bootstrap-custom.less',
        basePath: filesDir + '/bootstrap',
        out: outDir + "/lib/bootstrap/dist/css",
        compress: true,
    }));

    promises.push(compileTypescript());
    promises.push(artifact.importConfigs(filesDir, filesDir + "/wwwroot", [filesDir + '/artifacts.json', artifact.getDefaultGlob(filesDir)]));

    //Return composite promise
    return Promise.all(promises);
}

async function compileTypescript() {
    await tsc({
        projectFolder: filesDir
    });
}
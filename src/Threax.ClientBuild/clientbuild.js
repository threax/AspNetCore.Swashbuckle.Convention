"use strict";

var path = require('path');
var del = require('del');

var copy = require('threax-gulp-tk/copy.js');
var compileLess = require('threax-gulp-tk/less.js');
var compileJs = require('threax-gulp-tk/javascript.js');
var compileTypescript = require('threax-gulp-tk/typescript.js');

module.exports = function (rootDir, webroot, sharedSettings) {

    //Load library build modules
    var hrBootstrapBuild = require(rootDir + '/node_modules/HtmlRapier.Bootstrap/build');
    var jsonEditorBuild = require(rootDir + '/node_modules/HtmlRapier.json-editor/build');
    var htmlRapier = require(rootDir + '/node_modules/HtmlRapier/build');
    var htmlRapierWidgets = require(rootDir + '/node_modules/HtmlRapier.Widgets/build');
    var hrHalcyon = require(rootDir + '/node_modules/HtmlRapier.Halcyon/build');

    if (sharedSettings === undefined) {
        sharedSettings = {};
    }

    if (sharedSettings.minify === undefined) {
        sharedSettings.minify = true;
    }

    if (sharedSettings.concat === undefined) {
        sharedSettings.concat = true;
    }

    var libDir = webroot + "lib/";

    //File Copy
    copy({
        libs: ["./node_modules/jsns/jsns.js"],
        baseName: './node_modules/jsns',
        dest: libDir
    });

    //Compile modules
    htmlRapier(rootDir, libDir, sharedSettings);
    htmlRapierWidgets(rootDir, libDir, sharedSettings);
    jsonEditorBuild(rootDir + '/node_modules/json-editor/', libDir);
    hrBootstrapBuild(rootDir, libDir, sharedSettings);
    hrHalcyon(rootDir, libDir, sharedSettings);

    //Client Side ts
    compileTypescript({
        libs: [
            rootDir + "/Client/Libs/**/*.ts",
        ],
        runners: false,
        dest: libDir,
        sourceRoot: rootDir + "/Client/Libs/",
        namespace: "clientlibs",
        output: "ClientLibs",
        concat: sharedSettings.concat,
        minify: sharedSettings.minify
    });

    //Additional Styles
    compileLess({
        files: [
        rootDir + '/Client/Styles/*.less'
        ],
        dest: libDir + '/css',
        importPaths: [path.join(rootDir + '/Client/Styles/'), path.join(rootDir, "node_modules/bootstrap/less")],
    });

    //Views
    compileTypescript({
        libs: [
            rootDir + "/Views/**/*.ts",
            "!**/*.intellisense.js"
        ],
        runners: true,
        dest: libDir + '/views',
        sourceRoot: rootDir + "/Views"
    });
};
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

    //var webroot = __dirname + "/wwwroot/";

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
    htmlRapier(__dirname, libDir, sharedSettings);
    htmlRapierWidgets(__dirname, libDir, sharedSettings);
    jsonEditorBuild(__dirname + '/node_modules/json-editor/', libDir);
    hrBootstrapBuild(__dirname, libDir, sharedSettings);
    hrHalcyon(__dirname, libDir, sharedSettings);

    //Client Side ts
    compileTypescript({
        libs: [
            __dirname + "/Client/Libs/**/*.ts",
        ],
        runners: false,
        dest: libDir,
        sourceRoot: __dirname + "/Client/Libs/",
        namespace: "clientlibs",
        output: "ClientLibs",
        concat: sharedSettings.concat,
        minify: sharedSettings.minify
    });

    //Additional Styles
    compileLess({
        files: [
        __dirname + '/Client/Styles/*.less'
        ],
        dest: libDir + '/css',
        importPaths: [path.join(__dirname + '/Client/Styles/'), path.join(__dirname, "node_modules/bootstrap/less")],
    });

    //Views
    compileTypescript({
        libs: [
            __dirname + "/Views/**/*.ts",
            "!**/*.intellisense.js"
        ],
        runners: true,
        dest: libDir + '/views',
        sourceRoot: __dirname + "/Views"
    });
};
"use strict";

var path = require('path');
var del = require('del');

var copy = require('threax-gulp-tk/copy.js');
var compileLess = require('threax-gulp-tk/less.js');
var compileJs = require('threax-gulp-tk/javascript.js');
var compileTypescript = require('threax-gulp-tk/typescript.js');

module.exports = function (rootDir, webroot, sharedSettings) {
    if (sharedSettings === undefined) {
        sharedSettings = {};
    }

    if (sharedSettings.minify === undefined) {
        sharedSettings.minify = true;
    }

    if (sharedSettings.concat === undefined) {
        sharedSettings.concat = true;
    }

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
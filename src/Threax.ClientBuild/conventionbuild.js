"use strict";

var path = require('path');
var del = require('del');

var copy = require('threax-gulp-tk/copy.js');
var compileLess = require('threax-gulp-tk/less.js');
var compileJs = require('threax-gulp-tk/javascript.js');
var compileTypescript = require('threax-gulp-tk/typescript.js');
var clientBuild = require('./clientbuild.js');

module.exports = function (rootDir, libDir, sharedSettings) {

    //Load library build modules
    var hrBootstrapBuild = require(rootDir + '/node_modules/HtmlRapier.Bootstrap/build');
    var jsonEditorBuild = require(rootDir + '/node_modules/HtmlRapier.json-editor/build');
    var htmlRapier = require(rootDir + '/node_modules/HtmlRapier/build');
    var htmlRapierWidgets = require(rootDir + '/node_modules/HtmlRapier.Widgets/build');
    var hrHalcyon = require(rootDir + '/node_modules/HtmlRapier.Halcyon/build');
    var halcyonExplorerBuild = require(rootDir + '/node_modules/htmlrapier.halcyon-explorer/build.js');

    if (sharedSettings === undefined) {
        sharedSettings = {};
    }

    if (sharedSettings.minify === undefined) {
        sharedSettings.minify = true;
    }

    if (sharedSettings.concat === undefined) {
        sharedSettings.concat = true;
    }

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
    clientBuild(rootDir, libDir, sharedSettings);
    halcyonExplorerBuild(__dirname, libDir, sharedSettings);
};
"use strict";

var gulp = require("gulp");
var path = require('path');

var copy = require('threax-gulp-tk/copy.js');
var compileLess = require('threax-gulp-tk/less.js');
var compileJs = require('threax-gulp-tk/javascript.js');
var compileTypescript = require('threax-gulp-tk/typescript.js');

//Load library build modules
var htmlRapier = require('./node_modules/HtmlRapier/build');
var hrBootstrapBuild = require('./node_modules/HtmlRapier.Bootstrap/build');
var jsonEditorBuild = require('./node_modules/HtmlRapier.json-editor/build');

var webroot = __dirname + "/wwwroot/";

gulp.task("default", function () {
    build();
});

gulp.task("debug", function () {
    var sharedSettings = {
        minify: false,
    };

    build(sharedSettings);
});

function build(sharedSettings) {
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

    //jsns Copy
    copy({
        libs: ["./node_modules/jsns/jsns.js",],
        baseName: './node_modules/jsns',
        dest: libDir
    });

    //Bootstrap
    copy({
        libs: ["./node_modules/bootstrap/dist/**/*",
               "!./node_modules/bootstrap/dist/js/**/*"],
        baseName: './node_modules/bootstrap/dist',
        dest: libDir + 'bootstrap/'
    });

    //Compile modules
    htmlRapier(__dirname, libDir, sharedSettings);
    hrBootstrapBuild(__dirname, libDir, sharedSettings);
    jsonEditorBuild(__dirname + '/node_modules/json-editor/', libDir);

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
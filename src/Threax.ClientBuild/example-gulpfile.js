"use strict";

//Set webroot
var webroot = __dirname + "/wwwroot/";

var gulp = require("gulp");
var del = require('del');

//Load library build modules
var conventionbuild = require('./node_modules/threax-gulp-clientbuild/conventionbuild.js');

gulp.task("default", function () {
    build();
});

gulp.task("debug", function () {
    var sharedSettings = {
        minify: false,
    };

    build(sharedSettings);
});

gulp.task("clean", function () {
    del([webroot + "**"]);
});

function build(sharedSettings) {
    var libDir = webroot + "lib/";

    //Compile modules
    conventionbuild(__dirname, libDir, sharedSettings);
};
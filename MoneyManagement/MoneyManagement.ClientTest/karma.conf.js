// Karma configuration
// Generated on Wed Sep 28 2016 12:59:54 GMT+0700 (SE Asia Standard Time)

module.exports = function (config) {
    config.set({

        // base path that will be used to resolve all patterns (eg. files, exclude)
        basePath: '',


        // frameworks to use
        // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
        frameworks: ['jasmine'],

        // list of files / patterns to load in the browser
        files: [
            // Lib
            '../MoneyManagement.Web/Scripts/core-jquery.min.js',
            '../MoneyManagement.Web/Scripts/core-angular.min.js',
            '../MoneyManagement.Web/Scripts/select2.full.min.js',
            './node_modules/angular-mocks/angular-mocks.js',
            '../MoneyManagement.Web/Scripts/underscore-min.js',
            '../MoneyManagement.Web/Scripts/core-datepicker.min.js',
            '../MoneyManagement.Web/Scripts/core-libs.min.js',
            '../MoneyManagement.Web/Scripts/script.js',
            '../MoneyManagement.Web/Scripts/bootstrap.js',
            '../MoneyManagement.Web/Scripts/bootstrap.min.js',

            '../MoneyManagement.Web/Scripts/angular/angular-app.js',
            '../MoneyManagement.Web/Scripts/angular/directives/angular-directives.js',
            '../MoneyManagement.Web/Scripts/angular/services/angular-services.js',
            '../MoneyManagement.Web/Scripts/angular/filters/angular-filters.js',

            // Code
            '../MoneyManagement.Web/Areas/**/Scripts/**/*app.js',
            '../MoneyManagement.Web/Areas/**/Scripts/**/*.js',
            '../MoneyManagement.Web/Areas/**/Scripts/**/**/*.js',
            '../MoneyManagement.Web/Scripts/angular/**/*.js',
            '../MoneyManagement.Web/**/Templates/**/*.html',

            // Test
            './*-spec.js'
        ],


        // list of files to exclude
        exclude: [
        ],


        // preprocess matching files before serving them to the browser
        // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
        preprocessors: {
        },


        // test results reporter to use
        // possible values: 'dots', 'progress'
        // available reporters: https://npmjs.org/browse/keyword/karma-reporter
        reporters: ['progress'],


        // web server port
        port: 9876,


        // enable / disable colors in the output (reporters and logs)
        colors: true,


        // level of logging
        // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
        logLevel: config.LOG_INFO,


        // enable / disable watching file and executing tests whenever any file changes
        autoWatch: true,


        // start these browsers
        // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
        browsers: ['Chrome'],


        // Continuous Integration mode
        // if true, Karma captures browsers, runs the tests and exits
        singleRun: false,

        // Concurrency level
        // how many browser should be started simultaneous
        concurrency: Infinity
    })
}

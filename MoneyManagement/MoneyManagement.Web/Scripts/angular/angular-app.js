angular.module("mmHomeApp", ['ngResource', 'ngRoute'])
    .config(['$locationProvider', function ($locationProvider) {
        $locationProvider.html5Mode(false);
    }]);
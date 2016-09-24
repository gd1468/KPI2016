'use strict';

angular.module('expenditureApp', ['ngRoute', 'mmHomeApp', 'expenditureServices', 'expenditureDirectives'])
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/', {
            templateUrl: '/Areas/Finance/Templates/Expenditure/Main.html',
            controller: 'expenditureController',
            resolve: {
                data: [
                    'cultureService',
                    'authenticationService',
                    '$window',
                    '$rootScope',
                    '$q', function (cultureService, authenticationService, $window, $rootScope, $q) {
                        if ($window.sessionStorage["userInfo"]) {
                            $rootScope.user = JSON.parse($window.sessionStorage["userInfo"]);
                        } else {
                            $window.location.href = "/";
                        }
                        return $q.all({
                            culture: cultureService.cultures
                        }).then(function (response) {
                            $rootScope.culture = response.culture.CulturePresentation;
                        });
                    }]
            }
        })
        .otherwise({
            redirectTo: '/'
        });
    }])
    .run(['$rootScope', 'cultureService', 'authenticationService', '$window', function ($rootScope, cultureService, authenticationService, $window) {

    }]);

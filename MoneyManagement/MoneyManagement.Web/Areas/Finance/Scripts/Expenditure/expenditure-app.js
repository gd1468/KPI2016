'use strict';

angular.module('expenditureApp', ['ngRoute', 'mmHomeApp', 'expenditureServices'])
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/', {
            templateUrl: '/Areas/Finance/Templates/Expenditure/Main.html',
            controller: 'expenditureController'
        })
        .otherwise({
            redirectTo: '/'
        });
    }])
    .run(['$rootScope', 'cultureService', 'authenticationService', '$window', function ($rootScope, cultureService, authenticationService, $window) {
        cultureService.cultures.then(function (response) {
            $rootScope.culture = response.CulturePresentation;
        });
        if ($window.sessionStorage["userInfo"]) {
            $rootScope.user = JSON.parse($window.sessionStorage["userInfo"]);
        } else {
            $window.location.href = "/";
        }
    }]);

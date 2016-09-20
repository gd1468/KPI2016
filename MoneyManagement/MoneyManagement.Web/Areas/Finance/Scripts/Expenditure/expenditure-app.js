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
    .run(['$rootScope', 'cultureService', function ($rootScope, cultureService) {
        cultureService.cultures.then(function (response) {
            $rootScope.culture = response.CulturePresentation;
        });
    }]);

'use strict';

angular.module('expenditureApp', ['ngRoute', 'mmHomeApp'])
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/', {
            templateUrl: '/Areas/Finance/Templates/Expenditure/Main.html',
            controller: 'expenditureController'
        })
        .otherwise({
            redirectTo: '/'
        });
    }]);

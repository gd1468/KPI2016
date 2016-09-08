(function () {
    'use strict';
    angular.module("mmHomeApp", ["ngRoute"])
        .directive("mmPageHeader", function () {
            return {
                restrict: 'E',

                transclude: true,
                replace: true,
                scope: {

                },
                templateUrl: '/Scripts/angular/templates/mmPageHeader.html'
            }
        })
        .directive('mmLoginForm', function () {
            return {
                restrict: 'E',
                transclude: true,
                replace: true,
                scope: {
                    userName: "=",
                    passWord: "="
                },
                templateUrl: '/Scripts/angular/templates/mmLoginForm.html',
                controller: ['$scope', function ($scope) {
                    $scope.submit = function() {
                        window.alert("Welcome " + $scope.userName);
                    };
                }]
            }
        });
})();
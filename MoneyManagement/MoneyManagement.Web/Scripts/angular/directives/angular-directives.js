(function () {
    'use strict';
    angular.module("mmHomeApp", ['ngResource', 'ngRoute'])
        .directive("mmPageHeader", function () {
            return {
                restrict: 'E',
                transclude: true,
                replace: true,
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
                controller: ['$scope', '$resource', function ($scope, $resource) {
                    var api = $resource("/api/User");
                    $scope.submit = function () {
                        var user = {
                            userName: $scope.userName,
                            passWord: $scope.passWord
                        };

                        api.get(user).then(function(response) {
                            window.alert("Welcome " + response.data);
                        });
                        
                    };
                }]
            }
        });
})();
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
        .directive('mmLoginForm', ['$resource', '$location', function ($resource, $location) {
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
                    $scope.message = "Enter your username and password to log on";
                    var api = $resource("/api/User");
                    $scope.submit = function () {
                        var user = {
                            userName: $scope.userName,
                            passWord: $scope.passWord
                        };

                        api.get(user).$promise.then(function (response) {
                            if (response.User.UserName) {
                                var landingUrl = $location.path("/Finance/Expenditure");
                                $location.href = landingUrl;
                            }
                            $scope.message = "User name or password is invalid";
                        });

                    };
                }]
            }
        }]);
})();
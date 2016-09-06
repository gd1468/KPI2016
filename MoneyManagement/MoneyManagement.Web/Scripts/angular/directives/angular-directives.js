(function () {
    'use strict';
    angular.module("mmHomeApp", ["ngRoute"])
        .directive("mmPageHeader",
             function () {
                 return {
                     restrict: 'E',

                     transclude: true,
                     replace: true,
                     scope: {

                     },
                     templateUrl: '/Scripts/angular/templates/mmPageHeader.html'
                 }
             }
        );
})();
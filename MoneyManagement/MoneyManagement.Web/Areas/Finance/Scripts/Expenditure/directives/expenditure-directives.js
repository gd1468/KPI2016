'use strict';

(function () {
    angular.module('expenditureDirectives', [])
    .directive('addAccount', [
        function () {
            return {
                restrict: 'A',
                templateUrl: '/Areas/Finance/Templates/Expenditure/AddAccount.html'
            };
        }
    ]);
})();

(function () {
    'use strict';
    angular.module('mmHomeApp')
        .filter('lookupFilter', function () {
            return function (array, query) {
                var result = [];
                angular.forEach(array, function (item) {
                    if (item.DisplayName.indexOf(query) !== -1 || item.Balance.toString().indexOf(query) !== -1) {
                        result.push(item);
                    }
                });
                return result;
            };
        });
})();
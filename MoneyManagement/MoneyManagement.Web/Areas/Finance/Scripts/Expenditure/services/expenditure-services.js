(function () {
    angular.module('expenditureServices', [])
        .factory('expenditureService', ['$resource', 'accountService', 'budgetService', function ($resource, accountService, budgetService) {

            return {

            };
        }])
        .service('accountService', ['$resource', function ($resource) {
            var accountApi = $resource('/api/AccountApi/', {});
            this.accounts = accountApi.get().$promise;
            this.getAccountById = function (id) {
                return accountApi.get({ id: id });
            };
        }])
        .factory('budgetService', ['$resource', function ($resource) {
            var budgetApi = $resource('/api/AccountApi/', {});
            var getBudgetById = function (id) {
                return budgetApi.get({ id: id });
            }
            return {
                budgets: budgetApi.get().$promise,
                getBudgetById: getBudgetById
            }
        }]);
})();
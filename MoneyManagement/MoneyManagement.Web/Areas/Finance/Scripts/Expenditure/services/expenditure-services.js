(function () {
    angular.module('expenditureServices', [])
        .factory('expenditureService', ['$resource', 'accountService', 'budgetService', function ($resource, accountService, budgetService) {

            return {

            };
        }])
        .service('accountService', ['$resource', function ($resource) {
            var accountApi = $resource('/api/Expenditure/Account/:id', { id: "@id" });
            this.accounts = accountApi.query().$promise;
            this.getAccountById = function (id) {
                return accountApi.get({ id: id });
            };
        }])
        .factory('budgetService', ['$resource', function ($resource) {

            return {

            }
        }]);
})();
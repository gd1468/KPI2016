(function () {
    angular.module('expenditureServices', [])
        .factory('expenditureService', ['$resource', 'accountService', 'budgetService', function ($resource, accountService, budgetService) {
            var url = '/api/ExpenditureApi';
            var createNewExpenditureRecord = function (model) {
                var expenditureApi = $http.post(url, model);
                return expenditureApi;
            };

            return {
                createNewExpenditureRecord: createNewExpenditureRecord
            };
        }])
        .service('accountService', ['$resource', function ($resource) {
            var accountApi = $resource('/api/AccountApi/', {
                params: {
                    cultureId: '@cultureId'
                }
            });

            var accounts = function (cultureId) {
                return accountApi.get({ cultureId: cultureId }).$promise;
            };

            this.accounts = accounts;

            this.getAccountById = function (id, cultureId) {
                return accountApi.get({ id: '@id', cultureId: cultureId });
            };
        }])
        .factory('budgetService', ['$resource', function ($resource) {
            var budgetApi = $resource('/api/BudgetApi/', {
                params: {
                    cultureId: '@cultureId'
                }
            });
            var getBudgetById = function (id) {
                return budgetApi.get({ id: id });
            }
            return {
                budgets: function (cultureId) {
                    return budgetApi.get({ cultureId: cultureId }).$promise;
                },
                getBudgetById: getBudgetById
            }
        }])
        .factory('cultureService', ['$resource', function ($resource) {
            var cultureApi = $resource('/api/CultureApi/', {});
            return {
                cultures: cultureApi.get().$promise
            }
        }]);
})();
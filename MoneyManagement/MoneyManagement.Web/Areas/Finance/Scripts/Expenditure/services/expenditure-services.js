(function () {
    angular.module('expenditureServices', [])
        .factory('expenditureService', ['$http', function ($http) {
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

            var accounts = function (cultureId, userId) {
                return accountApi.get({ cultureId: cultureId, userId: userId }).$promise;
            };

            this.accounts = accounts;

            this.getAccountById = function (id, cultureId) {
                return accountApi.get({ id: '@id', cultureId: cultureId });
            };

            this.createNewAccount = function (model) {
                return accountApi.save(model).$promise;
            };
        }])
        .factory('budgetService', ['$resource', function ($resource) {
            var budgetApi = $resource('/api/BudgetApi/', {
                params: {
                    cultureId: '@cultureId',
                    userId: '@user'
                }
            });
            var getBudgetById = function (id) {
                return budgetApi.get({ id: id });
            }
            return {
                budgets: function (cultureId, user) {
                    return budgetApi.get({ cultureId: cultureId, userId: user }).$promise;
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
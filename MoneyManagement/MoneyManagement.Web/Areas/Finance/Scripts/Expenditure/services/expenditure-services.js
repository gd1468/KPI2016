(function () {
    angular.module('expenditureServices', [])
        .factory('expenditureService', ['$http', function ($http) {
            var url = '/api/ExpenditureApi';
            var createNewExpenditureRecord = function (model) {
                var expenditureApi = $http.post(url, model);
                return expenditureApi;
            };

            var depositExistingAccount = function (model) {
                var expenditureApi = $http.put(url, model);
                return expenditureApi;
            };

            var expenditures = function (cultureId, userId) {
                return $http.get(url, { params: { cultureId: cultureId, userId: userId } });
            }

            var deleteExpenditure = function (expenditureIds, userId, cultureId) {
                return $http.delete(url, {
                    params: {
                        expenditureIds: expenditureIds,
                        userId: userId,
                        cultureId: cultureId
                    }
                });
            }

            var updateExpenditure = function (model) {
                var expenditureApi = $http.put(url + "/Edit", model);
                return expenditureApi;
            }
            return {
                createNewExpenditureRecord: createNewExpenditureRecord,
                depositExistingAccount: depositExistingAccount,
                expenditures: expenditures,
                deleteExpenditure: deleteExpenditure,
                updateExpenditure: updateExpenditure
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

            this.deleteAccount = function (accountIds) {
                return accountApi.remove({ accountIds: accountIds }).$promise;
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
            var createNewBudget = function (model) {
                return budgetApi.save(model).$promise;
            };

            var deleteBudget = function (budgetIds) {
                return budgetApi.remove({ budgetIds: budgetIds }).$promise;
            };
            return {
                budgets: function (cultureId, user) {
                    return budgetApi.get({ cultureId: cultureId, userId: user }).$promise;
                },
                getBudgetById: getBudgetById,
                createNewBudget: createNewBudget,
                deleteBudget: deleteBudget
            }
        }])
        .factory('cultureService', ['$resource', function ($resource) {
            var cultureApi = $resource('/api/CultureApi/', {});
            return {
                cultures: cultureApi.get().$promise
            }
        }]);
})();
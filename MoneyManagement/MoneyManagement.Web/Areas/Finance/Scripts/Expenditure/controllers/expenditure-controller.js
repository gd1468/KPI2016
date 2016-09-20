'use strict';

angular.module('expenditureApp')
    .controller('expenditureController', [
        '$scope',
        'expenditureService',
        'accountService',
        'budgetService',
        '$rootScope',
        '$timeout', function ($scope, expenditureService, accountService, budgetService, $rootScope, $timeout) {
            $timeout(function () {
                accountService.accounts($rootScope.culture.KeyId).then(function (response) {
                    $scope.accounts = response.AccountPresentations;
                });

                budgetService.budgets($rootScope.culture.KeyId).then(function (response) {
                    $scope.budgets = response.BudgetPresentations;
                });
            });

            var createExpenditureModel = function () {
                return {
                    amount: null,
                    budget: null,
                    account: null,
                    expenditureDate: null,
                    description: null
                };
            };

            var resetForm = function (form) {

            };

            $scope.model = createExpenditureModel();

            $scope.expenditureTabs = [
                { tabName: 'Records', tabId: 1, include: '/Areas/Finance/Templates/Expenditure/Records.html', loadedTab: true },
                { tabName: 'Accounts', tabId: 2, include: '/Areas/Finance/Templates/Expenditure/Accounts.html', loadedTab: true },
                { tabName: 'Budget', tabId: 3, include: '/Areas/Finance/Templates/Expenditure/Budget.html', loadedTab: true }
            ];

            $scope.setSelected = function (args) {

            };

            $scope.onSubmit = function (form) {
                if (form && form.isValid) {
                    expenditureService.createNewExpenditureRecord($scope.model).then(function (response) {
                        $scope.resetForm(form);
                    });
                } else {
                    window.alert("invalid");
                }
            };

            $scope.resetForm = function (form) {
                if (form) {
                    form.$setPristine();
                    form.$setUntouched();
                    $(".select2").select2("data", null);
                    $scope.model = createExpenditureModel();
                }
            };
        }]);
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
                accountService.accounts($rootScope.culture.KeyId, $scope.user.keyId).then(function (response) {
                    $scope.accounts = response.AccountPresentations;
                });

                budgetService.budgets($rootScope.culture.KeyId, $scope.user.keyId).then(function (response) {
                    $scope.budgets = response.BudgetPresentations;
                });
            });

            $scope.expenditureTypes = [
                { DisplayName: "Expense", isIncome: false },
                { DisplayName: "Income", isIncome: true }
            ];

            $scope.selectedExpenditureType = {};
            $scope.selectedExpenditureType.type = $scope.expenditureTypes[0];

            var createExpenditureModel = function () {
                return {
                    amount: null,
                    budget: null,
                    account: null,
                    expenditureDate: null,
                    description: null
                };
            };

            $scope.model = createExpenditureModel;

            $scope.expenditureTabs = [
                { tabName: 'Records', tabId: 1, include: '/Areas/Finance/Templates/Expenditure/Records.html', loadedTab: true },
                { tabName: 'Accounts', tabId: 2, include: '/Areas/Finance/Templates/Expenditure/Accounts.html', loadedTab: true },
                { tabName: 'Budget', tabId: 3, include: '/Areas/Finance/Templates/Expenditure/Budget.html', loadedTab: true }
            ];

            $scope.setSelected = function (args) {

            };

            $scope.onSubmit = function (form) {
                if (form && form.$valid) {
                    var model = {
                        amount: $scope.model.amount,
                        accountId: $scope.model.account.KeyId,
                        expenditureDate: $scope.model.expenditureDate,
                        description: $scope.model.description,
                        userId: $scope.user.keyId,
                        cultureId: $rootScope.culture.KeyId
                    };
                    if ($scope.selectedExpenditureType.type === $scope.expenditureTypes[0]) {
                        model.budgetId = $scope.model.budget.KeyId;
                        expenditureService.createNewExpenditureRecord(model).then(function (response) {
                            $scope.accounts = response.data.AccountPresentations;
                            $scope.budgets = response.data.BudgetPresentations;
                            $scope.resetForm(form);
                        });
                    } else {
                        expenditureService.depositExistingAccount(model).success(function (data) {
                            $scope.accounts = data.AccountPresentations;
                            $scope.resetForm(form);
                        });
                    }

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

            $scope.$on('update-accounts', function (event, data) {
                $scope.accounts = data;
                if ($scope.model) {
                    $scope.model.account = null;
                }
                $("#accountOption").select2("data", null);
            });

            $scope.$on('update-budgets', function (event, data) {
                $scope.budgets = data;
                if ($scope.model) {
                    $scope.model.budget = null;
                }
                $("#budgetOption").select2("data", null);
            });
        }]);
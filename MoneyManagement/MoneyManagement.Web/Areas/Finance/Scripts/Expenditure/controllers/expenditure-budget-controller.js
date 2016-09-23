'use strict';

angular.module('expenditureApp')
    .controller('expenditureBudgetController', [
        '$scope',
        'expenditureService',
        'accountService',
        'budgetService',
        function ($scope, expenditureService, accountService, budgetService) {
            var createBudgetModel = function () {
                return {
                    name: null,
                    shortName: null,
                    startDate: null,
                    endDate: null,
                    total: null,
                    expensed: null,
                    balance: null
                };
            };
            $scope.newBudget = createBudgetModel();

            $scope.filter = {};

            $scope.toggleAll = function () {
                $scope.isAllSelected = !$scope.isAllSelected;
                angular.forEach($scope.budgets, function (budget) { budget.selected = $scope.isAllSelected; });
            };

            $scope.addNew = function () {
                $scope.isAdding = !$scope.isAdding;
            };

            $scope.cancelAddBudget = function () {
                $scope.isAdding = !$scope.isAdding;
            };

            $scope.addBudget = function (form) {
                if (form && form.$valid) {
                    var model = {
                        name: $scope.newBudget.name,
                        shortName: $scope.newBudget.shortName,
                        startDate: $scope.newBudget.startDate,
                        endDate: $scope.newBudget.endDate,
                        total: $scope.newBudget.total,
                        expensed: $scope.newBudget.expensed,
                        balance: $scope.newBudget.total,
                        userId: $scope.user.keyId,
                        cultureId: $scope.culture.KeyId
                    };
                    budgetService.createNewBudget(model).then(function (response) {
                        $scope.resetForm(form);
                        $scope.newBudget = createBudgetModel();
                        $scope.isAdding = !$scope.isAdding;
                        $scope.$emit('update-budgets', response.BudgetPresentations);
                    });
                } else {
                    window.alert("invalid");
                }
            };

            $scope.deleteBudget = function () {
                var confirm = window.confirm("Are you sure?");
                if (confirm) {
                    var budgetIds = [];
                    angular.forEach($scope.budgets, function (budget) {
                        if (budget.selected) {
                            budgetIds.push(budget.KeyId);
                        }
                    });
                    budgetService.deleteBudget(budgetIds).then(function (response) {
                        if (response.EffectiveRows > 0) {
                            var budgets = _.filter($scope.budgets, function (budget) { return angular.isUndefined(budget.selected) || budget.selected == null || !budget.selected });
                            $scope.$emit('update-budgets', budgets);
                        }
                    });
                }
            };

            $scope.isDisabledDelete = function () {
                var budgets = _.filter($scope.budgets, function (budget) { return budget.selected });
                return budgets.length === 0;
            };

            $scope.lookupFilter = function (filter) {
                return function (budget) {
                    if (angular.isUndefined(filter)) {
                        return budget;
                    }
                    return budget.DisplayName.indexOf(filter) !== -1;
                };
            };

            $scope.onStartDateChange = function (startDate) {
                var date = new Date(startDate);
                if (angular.isDate(date)) {
                    var endDate = new Date(date.getFullYear(), date.getMonth() + 1, date.getDate());
                    $scope.newBudget.endDate = endDate;
                }
            };
        }
    ]);
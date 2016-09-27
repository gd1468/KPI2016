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
    ])
    .directive('addBudget', [
        function () {
            return {
                restrict: 'A',
                templateUrl: '/Areas/Finance/Templates/Expenditure/AddBudget.html'
            };
        }
    ])
    .directive('expenditureHistory', ['expenditureService',
        function (expenditureService) {
            return {
                restrict: 'A',
                templateUrl: '/Areas/Finance/Templates/Expenditure/ExpenditureHistory.html',
                scope: {
                    accounts: "=",
                    budgets: "=",
                    userId: "@",
                    expenditures: "=",
                    cultureId: "@",
                    turnOffHistory: "&"
                },
                compile: function () {
                    return {
                        pre: function (scope) {

                        },
                        post: function (scope) {
                            scope.isDisabledDelete = function () {
                                var expenditures = _.filter(scope.expenditures, function (expenditure) { return expenditure.selected });
                                return expenditures.length === 0;
                            }

                            scope.toggleAll = function () {
                                scope.isAllSelected = !scope.isAllSelected;
                                angular.forEach(scope.expenditures, function (expenditure) { expenditure.selected = scope.isAllSelected; });
                            };

                            var padLeft = function (pad, str, chr) {
                                var ans = pad.substring(chr, pad.length - str.length) + str;
                                return ans;
                            };

                            scope.lookupFilter = function (filter) {
                                return function (expenditure) {
                                    if (angular.isUndefined(filter)) {
                                        return expenditure;
                                    }
                                    var amount = expenditure.Amount.toString().indexOf(filter) !== -1;
                                    var budget = expenditure.Budget ? expenditure.Budget.DisplayName.indexOf(filter) !== -1 : false;
                                    var account = expenditure.Account.DisplayName.indexOf(filter) !== -1;
                                    var description = expenditure.Description ? expenditure.Description.indexOf(filter) !== -1 : false;
                                    var type = (expenditure.Budget ? "Expense" : "Income").indexOf(filter) !== -1;
                                    var date = new Date(expenditure.ExpenditureDate);
                                    var expenditureDate = (padLeft("00", date.getDate().toString(), 0) + '/' + padLeft("00", (date.getMonth() + 1).toString(), 0) + '/' + date.getFullYear()).indexOf(filter) !== -1;

                                    return amount || budget || account || description || type || expenditureDate;
                                };
                            };

                            scope.deleteExpenditure = function () {
                                var confirm = window.confirm("Are you sure?");
                                if (confirm) {
                                    var expenditureIds = [];
                                    angular.forEach(scope.expenditures, function (expenditure) {
                                        if (expenditure.selected) {
                                            expenditureIds.push(expenditure.KeyId);
                                        }
                                    });
                                    expenditureService.deleteExpenditure(expenditureIds, scope.userId, scope.cultureId).then(function (response) {
                                        if (response.data.EffectiveRows > 0) {
                                            var expenditures = _.filter(scope.expenditures, function (expenditure) { return angular.isUndefined(expenditure.selected) || expenditure.selected == null || !expenditure.selected });
                                            scope.expenditures = expenditures;
                                            scope.accounts = response.data.AccountPresentations;
                                            scope.budgets = response.data.BudgetPresentations;
                                            scope.$emit('update-budgets', scope.budgets);
                                            scope.$emit('update-expenditures', scope.expenditures);
                                            scope.$emit('update-accounts', scope.accounts);
                                        }
                                    });
                                }
                            };

                            scope.selectExpenditure = function (expenditure, form) {
                                scope.selectedExpenditure = expenditure;
                                scope.isEditing = !scope.isEditing;
                                form.$setPristine();
                            }

                            scope.turnOffEditForm = function () {
                                scope.isEditing = !scope.isEditing;
                            }

                            scope.selectedExpenditure = {};

                            scope.resetForm = function (form) {
                                if (form) {
                                    form.$setPristine();
                                    form.$setUntouched();
                                    $(".select2").select2("data", null);
                                    scope.selectedExpenditure = {};
                                }
                            };

                            scope.updateExpenditure = function (form) {
                                if (form && form.$valid) {
                                    var model = {
                                        amount: scope.selectedExpenditure.Amount,
                                        accountId: scope.selectedExpenditure.Account.KeyId,
                                        expenditureDate: scope.selectedExpenditure.ExpenditureDate,
                                        description: scope.selectedExpenditure.Description,
                                        userId: scope.userId,
                                        cultureId: scope.cultureId,
                                        keyId: scope.selectedExpenditure.KeyId,
                                        budgetId: !scope.selectedExpenditure.IsIncome ? scope.selectedExpenditure.Budget.KeyId : null
                                    };

                                    expenditureService.updateExpenditure(model).then(function (response) {
                                        scope.accounts = response.data.AccountPresentations;
                                        scope.budgets = response.data.BudgetPresentations;
                                        scope.expenditures = response.data.ExpenditurePresentations;
                                        scope.$emit('update-budgets', scope.budgets);
                                        scope.$emit('update-expenditures', scope.expenditures);
                                        scope.$emit('update-accounts', scope.accounts);
                                        scope.resetForm(form);
                                        scope.isEditing = !scope.isEditing;
                                    });

                                } else {
                                    window.alert("invalid");
                                }
                            }
                        }
                    };
                },
                controller: function ($scope) {
                    this.getSelectedExpenditure = function () {
                        return $scope.selectedExpenditure;
                    }
                }
            };
        }
    ])
    .directive('editExpenditure', [function () {
        return {
            restrict: 'A',
            require: '^expenditureHistory',
            templateUrl: '/Areas/Finance/Templates/Expenditure/EditExpenditure.html',
            link: {
                pre: function (scope) {

                },
                post: function (scope, att, ele, ctrl) {
                    $('select.select2').select2();
                }
            }
        };
    }]);
})();

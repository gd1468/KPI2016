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
                        }
                    };
                }
            };
        }
    ]);
})();

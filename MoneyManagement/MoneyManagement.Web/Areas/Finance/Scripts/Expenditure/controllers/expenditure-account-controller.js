'use strict';

angular.module('expenditureApp')
    .controller('expenditureAccountController', [
        '$scope',
        'expenditureService',
        'accountService',
        'budgetService',
        function ($scope, expenditureService, accountService, budgetService) {
            var createAccountModel = function () {
                return {
                    amount: null,
                    name: null,
                    shortName: null
                };
            };

            $scope.filter = {};

            $scope.toggleAll = function () {
                $scope.isAllSelected = !$scope.isAllSelected;
                angular.forEach($scope.accounts, function (account) { account.selected = $scope.isAllSelected; });
            };

            $scope.addNew = function () {
                $scope.isAdding = !$scope.isAdding;
            };

            $scope.cancelAddAccount = function () {
                $scope.isAdding = !$scope.isAdding;
            };

            $scope.addAccount = function (form) {
                if (form && form.$valid) {
                    var model = {
                        amount: $scope.newAccount.amount,
                        name: $scope.newAccount.name,
                        shortName: $scope.newAccount.shortName,
                        userId: $scope.user.keyId,
                        cultureId: $scope.culture.KeyId
                    };
                    accountService.createNewAccount(model).then(function (response) {
                        $scope.resetForm(form);
                        $scope.newAccount = createAccountModel();
                        $scope.isAdding = !$scope.isAdding;
                        $scope.$emit('update-accounts', response.AccountPresentations);
                    });
                } else {
                    window.alert("invalid");
                }
            };

            $scope.deleteAccount = function () {
                var confirm = window.confirm("Are you sure?");
                if (confirm) {
                    var accountIds = [];
                    angular.forEach($scope.accounts, function (account) {
                        if (account.selected) {
                            accountIds.push(account.KeyId);
                        }
                    });
                    accountService.deleteAccount(accountIds).then(function (response) {
                        if (response.EffectiveRows > 0) {
                            var accounts = _.filter($scope.accounts, function (account) { return angular.isUndefined(account.selected) || account.selected == null || !account.selected });
                            $scope.$emit('update-accounts', accounts);
                        }
                    });
                }
            };

            $scope.isDisabledDelete = function () {
                var accounts = _.filter($scope.accounts, function (account) { return account.selected });
                return accounts.length === 0;
            };

            $scope.lookupFilter = function (filter) {
                return function (account) {
                    if (angular.isUndefined(filter)) {
                        return account;
                    }
                    return account.DisplayName.indexOf(filter) !== -1;
                };
            };
        }
    ]);
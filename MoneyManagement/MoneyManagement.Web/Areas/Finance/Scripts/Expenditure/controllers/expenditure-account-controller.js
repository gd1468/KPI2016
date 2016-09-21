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

            $scope.toggleAll = function () {
                $scope.isAllSelected = !$scope.isAllSelected;
                angular.forEach($scope.accounts, function (account) { account.selected = $scope.isAllSelected; });

            }

            $scope.addNew = function () {
                $scope.isAdding = !$scope.isAdding;
            }

            $scope.cancelAddAccount = function () {
                $scope.isAdding = !$scope.isAdding;
            }

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
                        $scope.accounts = response.AccountPresentations;
                        $scope.resetForm(form);
                        $scope.newAccount = createAccountModel();
                        $scope.isAdding = !$scope.isAdding;
                    });
                } else {
                    window.alert("invalid");
                }
            }
        }
    ]);
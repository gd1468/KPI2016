﻿'use strict';

angular.module('expenditureApp')
    .controller('expenditureController', ['$scope', function ($scope) {
        $scope.expenditureTabs = [
            { tabName: 'Records', tabId: 1, include: '/Areas/Finance/Templates/Expenditure/Records.html', loadedTab: true },
            { tabName: 'Accounts', tabId: 2, include: '/Areas/Finance/Templates/Expenditure/Accounts.html', loadedTab: true },
            { tabName: 'Budget', tabId: 3, include: '/Areas/Finance/Templates/Expenditure/Budget.html', loadedTab: true }
        ];

        $scope.setSelected = function (args) {
            
        };
    }]);
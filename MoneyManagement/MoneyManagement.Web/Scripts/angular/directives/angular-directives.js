(function () {
    'use strict';
    angular.module("mmHomeApp")
        .directive("mmPageHeader", function () {
            return {
                restrict: 'E',
                transclude: true,
                replace: true,
                templateUrl: '/Scripts/angular/templates/mmPageHeader.html'
            }
        })
        .directive('mmLoginForm', ['$resource', '$location', '$window', function ($resource, $location, $window) {
            return {
                restrict: 'E',
                transclude: true,
                replace: true,
                scope: {
                    userName: "=",
                    passWord: "="
                },
                templateUrl: '/Scripts/angular/templates/mmLoginForm.html',
                controller: ['$scope', function ($scope) {
                    $scope.message = "Enter your username and password to log on";
                    var api = $resource("/api/User");
                    $scope.submit = function () {
                        var user = {
                            userName: $scope.userName,
                            passWord: $scope.passWord
                        };

                        api.get(user).$promise.then(function (response) {
                            if (response.User.UserName) {
                                $window.location.href = "/Finance/Expenditure#/";
                            } else {
                                $scope.message = "User name or password is invalid";
                            }

                        });

                    };
                }]
            }
        }])
    .directive('mmTabControl', [function () {
        return {
            restrict: 'E',
            transclude: true,
            replace: true,
            scope: true,
            template: '<div><nav>' +
                '     <ul class="nav nav-tabs" ng-class="{ \'mo-lightweight-tabs\': lightweight }">' +
                '         <li class="tab-title" ng-class="{active: i.active}" ng-repeat="i in tabItems" ng-attr-id="{{i.tabId}}">' +
                '             <a href="" ng-click="select(i)">{{i.tabTitle}} <span class="badge bump-right" ng-if="i.showTabBadge">{{i.tabBadge}}</span></a>' +
                '             <span ng-if="lightweight" class="mo-lightweight-tab"></span>' +
                '     </li></ul>' +
                '</nav><div ng-transclude></div></div>',

            controller: ['$scope', '$element', '$attrs', '$parse', function ($scope, $element, $attrs, $parse) {
                $scope.tabItems = [];
                $scope.selectedTab = '';
                $scope.lightweight = $attrs.lightweight;
                $scope.select = function (item) {

                    //cached loaded tabs
                    if (angular.isDefined($scope.loadedTabs)) {
                        if (angular.isUndefined($scope.loadedTabs[item.tabId])) {
                            $scope.loadedTabs[item.tabId] = false;
                        }
                        $scope.loadedTabs[item.tabId] = true;
                    }

                    var _item = $element.find("[id='" + item.tabId + "']");

                    if (_item.hasClass('disabled') !== true) {
                        angular.forEach($scope.tabItems, function (i) {
                            i.active = false;
                        });

                        item.active = true;

                        if ($attrs.onSelected) {
                            $parse($attrs.onSelected)($scope, { args: { selectedItem: item } });
                            $scope.selectedTab = item.tabId;
                        }
                        $scope.$broadcast('bcctab.tabActivated', { tab: item });
                    }
                };

                this.addTabItem = function (item) {
                    $scope.tabItems.push(item);
                    if ($scope.tabItems.length == 1) {
                        $scope.$evalAsync(function () {
                            $scope.select(item);
                        });
                    }

                };

                this.isTabSelected = function (id) {
                    var result = false;
                    angular.forEach($scope.tabItems, function (item, index) {

                        if (item.tabId === id && item.active == true) {
                            result = true;
                        }
                    });
                    return result;
                };

                this.selectTab = function (id) {
                    var tabItem = _.where($scope.tabItems, { tabId: id });
                    if (tabItem.length) {
                        $scope.select(tabItem[0]);
                    }
                }
            }]
        };
    }
    ])
    .directive('mmTabItem', [
            function () {
                return {
                    restrict: 'E',
                    transclude: true,
                    replace: true,
                    template: '<div ng-show="isTabSelected()" ng-transclude></div>',
                    require: '^mmTabControl',
                    scope: true,
                    link: function (scope, elem, attrs, tabControl) {
                        var hideTab = false;
                        if (attrs.hide) {
                            hideTab = scope.$eval(attrs.hide);
                            if (hideTab) {
                                return;
                            }
                        }
                        var tab = {
                            active: false,
                            bccTabItem: attrs.bccTabItem,
                            tabId: attrs.tabId,
                            tabTitle: attrs.tabTitle,
                            tabBadge: '',
                            showTabBadge: false
                        };

                        tabControl.addTabItem(tab);
                        scope.isTabSelected = function () {
                            return tabControl.isTabSelected(attrs.tabId);
                        };

                        if (attrs.hasOwnProperty('tabBadge')) {
                            attrs.$observe('tabBadge', function (newValue) {
                                tab.tabBadge = newValue;
                                tab.showTabBadge = !!newValue;
                            });
                        }
                    }
                };
            }
    ]);
})();
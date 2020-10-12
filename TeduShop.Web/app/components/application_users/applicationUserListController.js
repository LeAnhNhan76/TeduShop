(function (app) {
    // #region Constructor

    app.controller('applicationUserListController', applicationUserListController);
    applicationUserListController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$ngBootbox', '$filter'];

    // #endregion

    function applicationUserListController($scope, $rootScope, apiService, notificationService, resourceService, $ngBootbox, $filter) {
        // #region Properties and Variables
        
        // #region properties
        $scope.applicationUsers = [];
        $scope.page = 1;
        $scope.pageSize = 4;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.path = 'application_users';
        $scope.resourcePage = {};
        $scope.isAll = false;
        // #endregion

        // #region methods
        $scope.onGetApplicationUsers = onGetApplicationUsers;
        $scope.onSearch = onSearch;
        $scope.onDeleteApplicationUser = onDeleteApplicationUser;
        $scope.onSelectAll = onSelectAll;
        $scope.onDeleteApplicationUserMultiple = onDeleteApplicationUserMultiple;
        // #endregion

        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });

        $scope.$watch("applicationUsers", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);
        
        function onSearch() {
            onGetApplicationUsers();
        }

        function onGetApplicationUsers(page) {
            page = page || 1;
            var config = {
                params: {
                    page: page,
                    keyword: $scope.keyword
                }
            }
            console.log('config', config);
            apiService.get('/api/applicationuser/getpaging', config
                , function (result) {
                    console.log('result', result);
                    $scope.applicationUsers = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                }
                , function (error) {
                    console.log('Load application user fail');
                }
            );
        }

        $scope.onGetApplicationUsers();

        function onSelectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.applicationUsers, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.applicationUsers, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        function onDeleteApplicationUser(id) {
            $ngBootbox.confirm($scope.resourceShared.AreYouSureDeleteTheRecord).then(function () {
                var config = {
                    params: {
                       id: id
                    }
                }
                apiService.del('/api/applicationuser/delete', config
                    , function () {
                        notificationService.displaySuccess($scope.resourceShared.DeleteSuccess);
                        onSearch();
                    }
                    , function () {
                        notificationService.displayError($scope.resourceShared.DeleteFail);
                    }
                );
            });
        }

        function onDeleteApplicationUserMultiple() {
            $ngBootbox.confirm($scope.resourceShared.AreYouSureDeleteTheseRecords).then(function () {
                var lstId = [];
                $.each($scope.selected, function (i, item) {
                    lstId.push(item.ID);
                })
                var config = {
                    params: {
                        lstId: JSON.stringify(lstId)
                    }
                }
                apiService.del('/api/applicationuser/deletemulti', config
                    , function () {
                        notificationService.displaySuccess($scope.resourceShared.DeleteSuccess);
                        onSearch();
                    }
                    , function () {
                        notificationService.displayError($scope.resourceShared.DeleteFail);
                    }
                );
            });
        }

        // #endregion
    }
})(angular.module('tedushop.application_users'));
(function (app) {
    // #region Constructor

    app.controller('applicationRoleListController', applicationRoleListController);
    applicationRoleListController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$ngBootbox', '$filter'];

    // #endregion

    function applicationRoleListController($scope, $rootScope, apiService, notificationService, resourceService, $ngBootbox, $filter) {
        // #region Properties and Variables
        
        // #region properties
        $scope.applicationRoles = [];
        $scope.page = 1;
        $scope.pageSize = 4;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.path = 'application_roles';
        $scope.resourcePage = {};
        $scope.isAll = false;
        // #endregion

        // #region methods
        $scope.onGetApplicationRoles = onGetApplicationRoles;
        $scope.onSearch = onSearch;
        $scope.onDeleteApplicationRole = onDeleteApplicationRole;
        $scope.onSelectAll = onSelectAll;
        $scope.onDeleteApplicationRoleMultiple = onDeleteApplicationRoleMultiple;
        // #endregion

        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });

        $scope.$watch("applicationRoles", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);
        
        function onSearch() {
            onGetApplicationRoles();
        }

        function onGetApplicationRoles(page) {
            page = page || 1;
            var config = {
                params: {
                    page: page,
                    keyword: $scope.keyword
                }
            }
            console.log('config', config);
            apiService.get('/api/applicationrole/getpaging', config
                , function (result) {
                    console.log('result', result);
                    $scope.applicationRoles = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                }
                , function (error) {
                    console.log('Load application role fail');
                }
            );
        }

        $scope.onGetApplicationRoles();

        function onSelectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.applicationRoles, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.applicationRoles, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        function onDeleteApplicationRole(id) {
            $ngBootbox.confirm($scope.resourceShared.AreYouSureDeleteTheRecord).then(function () {
                var config = {
                    params: {
                       id: id
                    }
                }
                apiService.del('/api/applicationrole/delete', config
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

        function onDeleteApplicationRoleMultiple() {
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
                apiService.del('/api/applicationrole/deletemulti', config
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
})(angular.module('tedushop.application_roles'));
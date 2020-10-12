(function (app) {
    // #region Constructor

    app.controller('applicationGroupListController', applicationGroupListController);
    applicationGroupListController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$ngBootbox', '$filter'];

    // #endregion

    function applicationGroupListController($scope, $rootScope, apiService, notificationService, resourceService, $ngBootbox, $filter) {
        // #region Properties and Variables
        
        // #region properties
        $scope.applicationGroups = [];
        $scope.page = 1;
        $scope.pageSize = 4;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.path = 'application_groups';
        $scope.resourcePage = {};
        $scope.isAll = false;
        // #endregion

        // #region methods
        $scope.onGetApplicationGroups = onGetApplicationGroups;
        $scope.onSearch = onSearch;
        $scope.onDeleteApplicationGroup = onDeleteApplicationGroup;
        $scope.onSelectAll = onSelectAll;
        $scope.onDeleteApplicationGroupMultiple = onDeleteApplicationGroupMultiple;
        // #endregion

        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });

        $scope.$watch("applicationGroups", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);
        
        function onSearch() {
            onGetApplicationGroups();
        }

        function onGetApplicationGroups(page) {
            page = page || 1;
            var config = {
                params: {
                    page: page,
                    keyword: $scope.keyword
                }
            }
            console.log('config', config);
            apiService.get('/api/applicationgroup/getpaging', config
                , function (result) {
                    console.log('result', result);
                    $scope.applicationGroups = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                }
                , function (error) {
                    console.log('Load application group fail');
                }
            );
        }

        $scope.onGetApplicationGroups();

        function onSelectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.applicationGroups, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.applicationGroups, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        function onDeleteApplicationGroup(id) {
            $ngBootbox.confirm($scope.resourceShared.AreYouSureDeleteTheRecord).then(function () {
                var config = {
                    params: {
                       id: id
                    }
                }
                apiService.del('/api/applicationgroup/delete', config
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

        function onDeleteApplicationGroupMultiple() {
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
                apiService.del('/api/applicationgroup/deletemulti', config
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
})(angular.module('tedushop.application_groups'));
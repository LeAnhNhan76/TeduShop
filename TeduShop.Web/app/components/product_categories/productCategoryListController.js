(function (app) {
    // #region Constructor

    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$ngBootbox', '$filter'];

    // #endregion

    function productCategoryListController($scope, $rootScope, apiService, notificationService, resourceService, $ngBootbox, $filter) {
        // #region Properties and Variables
        
        // #region properties
        $scope.productCategories = [];
        $scope.page = 1;
        $scope.pageSize = 4;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.path = 'product_categories';
        $scope.resourcePage = {};
        $scope.isAll = false;
        // #endregion

        // #region methods
        $scope.onGetProductCategories = onGetProductCategories;
        $scope.onSearch = onSearch;
        $scope.onDeleteProductCategory = onDeleteProductCategory;
        $scope.onSelectAll = onSelectAll;
        $scope.onDeleteProductCategoryMultiple = onDeleteProductCategoryMultiple;
        // #endregion

        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });

        $scope.$watch("productCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);
        
        function onSearch() {
            onGetProductCategories();
        }

        function onGetProductCategories(page) {
            page = page || 1;
            var config = {
                params: {
                    page: page,
                    keyword: $scope.keyword
                }
            }
            apiService.get('/api/productcategory/getpaging', config
                , function (result) {
                    //if (result.data.TotalCount == 0) {
                    //    notificationService.displayWarning($scope.resourceShared.NoRecordFound);
                    //}
                    //else {
                    //    notificationService.displaySuccess($scope.resourceShared.Found + ' ' + result.data.TotalCount + ' ' + $scope.resourceShared.Record + '.');
                    //}
                    $scope.productCategories = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                }
                , function (error) {
                    console.log('Load product caregory fail');
                }
            );
        }

        $scope.onGetProductCategories();

        function onSelectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        function onDeleteProductCategory(id) {
            $ngBootbox.confirm($scope.resourceShared.AreYouSureDeleteTheRecord).then(function () {
                var config = {
                    params: {
                       id: id
                    }
                }
                apiService.del('/api/productcategory/delete', config
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

        function onDeleteProductCategoryMultiple() {
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
                apiService.del('/api/productcategory/deletemulti', config
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
})(angular.module('tedushop.product_categories'));
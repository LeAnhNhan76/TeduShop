(function (app) {
    // #region Constructor

    app.controller('productListController', productListController);
    productListController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$ngBootbox', '$filter'];

    // #endregion

    function productListController($scope, $rootScope, apiService, notificationService, resourceService, $ngBootbox, $filter) {
        // #region Properties and Variables

        // #region properties
        $scope.products = [];
        $scope.page = 1;
        $scope.pageSize = 4;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.path = 'products';
        $scope.resourcePage = {};
        $scope.isAll = false;
        // #endregion

        // #region methods
        $scope.onGetProducts = onGetProducts;
        $scope.onSearch = onSearch;
        $scope.onDeleteProduct = onDeleteProduct;
        $scope.onSelectAll = onSelectAll;
        $scope.onDeleteProductMultiple = onDeleteProductMultiple;
        // #endregion

        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });

        $scope.$watch("products", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function onSearch() 
            onGetProducts();
        }

        function onGetProducts(page) {
            page = page || 1;
            var config = {
                params: {
                    page: page,
                    keyword: $scope.keyword
                }
            }
            apiService.get('/api/product/getpaging', config
                , function (result) {
                    //if (result.data.TotalCount == 0) {
                    //    notificationService.displayWarning($scope.resourceShared.NoRecordFound);
                    //}
                    $scope.products = result.data.Items;
                    console.log('$scope.products', $scope.products);
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                }
                , function (error) {
                    console.log('Load product fail');
                }
            );
        }

        $scope.onGetProducts();

        function onSelectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        function onDeleteProduct(id) {
            $ngBootbox.confirm($scope.resourceShared.AreYouSureDeleteTheRecord).then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/product/delete', config
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

        function onDeleteProductMultiple() {
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
                apiService.del('/api/product/deletemulti', config
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
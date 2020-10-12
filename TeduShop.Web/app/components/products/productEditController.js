
(function (app) {

    // #region Constructor

    app.controller('productEditController', productEditController);
    productEditController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$state', '$stateParams', 'commonService'];

    // #endregion

    function productEditController($scope, $rootScope, apiService, notificationService, resourceService, $state, $stateParams, commonService) {

        // #region Properties and Variables

        // #region properties
        $scope.product = {};
        $scope.parentCategories = [];
        $scope.path = 'products';
        $scope.resourcePage = {};
        $scope.moreImages = [];
        // #endregion

        // #region methods
        $scope.onUpdateProduct = onUpdateProduct;
        $scope.onGetSeoTitle = onGetSeoTitle;
        $scope.onChooseImage = onChooseImage;
        $scope.onChooseMoreImage = onChooseMoreImage;
        // #endregion

        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });

        function onGetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function onLoadProductDetail() {
            apiService.get('/api/product/getbyid?id=' + $stateParams.id, null
                , function (result) {
                    $scope.product = result.data;
                    if ($scope.product.MoreImage != undefined && $scope.product.MoreImage != null) {
                        $scope.moreImages = JSON.parse($scope.product.MoreImage);
                        $scope.$applyAsync();
                    }
                }
                , function (error) {
                    notificationService.displayError(error.data);
                }
            )
        }
        onLoadProductDetail();

        function onUpdateProduct() {
            $scope.product.MoreImage = JSON.stringify($scope.moreImages);
            apiService.put('/api/product/update', $scope.product
                , function (result) {
                    notificationService.displaySuccess(result.data.Name + ' ' + $scope.resourceShared.Updated + '.');
                    $state.go('products');
                }
                , function (error) {
                    notificationService.displayError($scope.resourceShared.UpdateFail + '.');
                }
            );
        }
        function onGetProductCategories() {
            apiService.get('/api/productcategory/getall', null
                , function (result) {
                    $scope.productCategories = result.data;
                    $scope.$applyAsync();
                }
                , function (error) {
                    console.log('Cannot get list of product categories');
                }
            );
        }
        onGetProductCategories();

        function onChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
                $scope.$applyAsync();
            }
            finder.popup();
        }

        function onChooseMoreImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.moreImages.push(fileUrl);
                $scope.$applyAsync();
            }
            finder.popup();
        }

        // #endregion
    }

})(angular.module('tedushop.products'))
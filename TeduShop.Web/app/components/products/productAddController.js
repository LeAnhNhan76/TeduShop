(function (app) {

    // #region Constructor

    app.controller('productAddController', productAddController);
    productAddController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$state', 'commonService'];

    // #endregion

    function productAddController($scope, $rootScope, apiService, notificationService, resourceService, $state, commonService) {

        // #region Properties and Variables

        // #region properties
        $scope.product = {
            Status: true,
            Price: 0,
            OriginalPrice: 0,
        }
        $scope.productCategories = [];
        $scope.path = 'products';
        $scope.resourcePage = {};
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }
        $scope.moreImages = [];
        // #endregion

        // #region methods
        $scope.onAddProduct = onAddProduct;
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

        function onAddProduct() {
            $scope.product.MoreImage = JSON.stringify($scope.moreImages);
            apiService.post('/api/product/add', $scope.product
                , function (result) {
                    notificationService.displaySuccess(result.data.Name + ' ' + $scope.resourceShared.Added + '.');
                    $state.go('products');
                }
                , function (error) {
                    notificationService.displayError($scope.resourceShared.AddFail + '.');
                }
            );
        }
        function onGetProductCategories() {
            apiService.get('/api/productcategory/getall', null
                , function (result) {
                    $scope.productCategories = result.data;
                    $scope.product.CategoryID = $scope.productCategories[0].ID;
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
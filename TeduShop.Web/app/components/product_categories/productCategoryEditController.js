
(function (app) {

    // #region Constructor

    app.controller('productCategoryEditController', productCategoryEditController);
    productCategoryEditController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$state', '$stateParams', 'commonService'];

    // #endregion

    function productCategoryEditController($scope, $rootScope, apiService, notificationService, resourceService, $state, $stateParams, commonService) {

        // #region Properties and Variables
        
        // #region properties
        $scope.productCategory = {};
        $scope.parentCategories = [];
        $scope.path = 'product_categories';
        $scope.resourcePage = {};
        // #endregion

        // #region methods
        $scope.onUpdateProductCategory = onUpdateProductCategory;
        $scope.onGetSeoTitle = onGetSeoTitle;
        // #endregion

        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });

        function onGetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function onLoadProductCategoryDetail() {
            apiService.get('/api/productcategory/getbyid?id=' + $stateParams.id, null 
                , function (result) {
                    $scope.productCategory = result.data;
                }
                , function (error) {
                    notificationService.displayError(error.data);
                }
            )
        }
        onLoadProductCategoryDetail();

        function onUpdateProductCategory() {
            apiService.put('/api/productcategory/update', $scope.productCategory
                , function (result) {
                    console.log(result);
                    notificationService.displaySuccess(result.data.Name + ' ' + $scope.resourceShared.Updated + '.');
                    $state.go('product_categories');
                }
                , function (error) {
                    notificationService.displayError($scope.resourceShared.UpdateFail + '.');
                }
            );
        }
        function onLoadParentCategory() {
            apiService.get('/api/productcategory/getall', null
                , function (result) {
                    $scope.parentCategories = result.data;
                }
                , function (error) {
                    console.log('Cannot get list of parent product categories');
                }
            );
        }
        onLoadParentCategory();

        // #endregion
    }

})(angular.module('tedushop.product_categories'))
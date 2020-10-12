(function (app) {

    // #region Constructor
    
    app.controller('productCategoryAddController', productCategoryAddController);
    productCategoryAddController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$state', 'commonService'];

    // #endregion

    function productCategoryAddController($scope, $rootScope, apiService, notificationService, resourceService, $state, commonService) {

        // #region Properties and Variables

        // #region properties
        $scope.productCategory = {
            Status: true
        }
        $scope.parentCategories = [];
        $scope.path = 'product_categories';
        $scope.resourcePage = {};
        // #endregion

        // #region methods
        $scope.onAddProductCategory = onAddProductCategory;
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

        function onAddProductCategory() {
            apiService.post('/api/productcategory/add', $scope.productCategory
                , function (result) {
                    notificationService.displaySuccess(result.data.Name + ' ' + $scope.resourceShared.Added + '.');
                    $state.go('product_categories');
                }
                , function (error) {
                    notificationService.displayError($scope.resourceShared.AddFail + '.');
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
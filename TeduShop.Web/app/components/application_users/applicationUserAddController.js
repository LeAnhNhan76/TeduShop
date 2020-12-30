(function (app) {

    // #region Constructor
    
    app.controller('applicationUserAddController', applicationUserAddController);
    applicationUserAddController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$state', 'commonService'];

    // #endregion

    function applicationUserAddController($scope, $rootScope, apiService, notificationService, resourceService, $state, commonService) {

        // #region Properties and Variables

        // #region properties
        $scope.applicationUser = {
            
        }
        //$scope.parentCategories = [];
        $scope.path = 'application_users';
        $scope.resourcePage = {};
        // #endregion

        // #region methods
        $scope.onAddApplicationUser = onAddApplicationUser;
        // #endregion
        
        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });
        
        function onAddApplicationUser() {
            apiService.post('/api/applicationuser/add', $scope.applicationUser
                , function (result) {
                    notificationService.displaySuccess(result.data.FullName + ' ' + $scope.resourceShared.Added + '.');
                    $state.go('application_users');
                }
                , function (error) {
                    notificationService.displayError($scope.resourceShared.AddFail + '.');
                }
            );
        }  

        // #endregion
    }
   
})(angular.module('tedushop.application_users'))
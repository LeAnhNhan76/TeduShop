(function (app) {

    // #region Constructor
    
    app.controller('applicationRoleAddController', applicationRoleAddController);
    applicationRoleAddController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$state', 'commonService'];

    // #endregion

    function applicationRoleAddController($scope, $rootScope, apiService, notificationService, resourceService, $state, commonService) {

        // #region Properties and Variables

        // #region properties
        $scope.applicationRole = {
            
        }
        //$scope.parentCategories = [];
        $scope.path = 'application_roles';
        $scope.resourcePage = {};
        // #endregion

        // #region methods
        $scope.onAddApplicationRole = onAddApplicationRole;
        // #endregion
        
        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });
        
        function onAddApplicationRole() {
            apiService.post('/api/applicationrole/add', $scope.applicationRole
                , function (result) {
                    notificationService.displaySuccess(result.data.Name + ' ' + $scope.resourceShared.Added + '.');
                    $state.go('application_roles');
                }
                , function (error) {
                    notificationService.displayError($scope.resourceShared.AddFail + '.');
                }
            );
        } 

        // #endregion
    }
   
})(angular.module('tedushop.application_roles'))
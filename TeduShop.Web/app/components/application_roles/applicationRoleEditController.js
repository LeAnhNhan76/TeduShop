
(function (app) {

    // #region Constructor

    app.controller('applicationRoleEditController', applicationRoleEditController);
    applicationRoleEditController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$state', '$stateParams', 'commonService'];

    // #endregion

    function applicationRoleEditController($scope, $rootScope, apiService, notificationService, resourceService, $state, $stateParams, commonService) {

        // #region Properties and Variables
        
        // #region properties
        $scope.applicationRole = {};
        $scope.path = 'application_roles';
        $scope.resourcePage = {};
        // #endregion

        // #region methods

        $scope.onUpdateApplicationRole = onUpdateApplicationRole;

        // #endregion

        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });

        function onLoadApplicationRoleDetail() {
            apiService.get('/api/applicationrole/getbyid?id=' + $stateParams.id, null 
                , function (result) {
                    $scope.applicationRole = result.data;
                }
                , function (error) {
                    notificationService.displayError(error.data);
                }
            )
        }
        onLoadApplicationRoleDetail();

        function onUpdateApplicationRole() {
            apiService.put('/api/applicationrole/update', $scope.applicationRole
                , function (result) {
                    console.log(result);
                    notificationService.displaySuccess(result.data.Name + ' ' + $scope.resourceShared.Updated + '.');
                    $state.go('application_roles');
                }
                , function (error) {
                    notificationService.displayError($scope.resourceShared.UpdateFail + '.');
                }
            );
        }

        // #endregion
    }

})(angular.module('tedushop.application_roles'))
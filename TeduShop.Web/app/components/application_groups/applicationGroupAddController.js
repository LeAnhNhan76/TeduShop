(function (app) {

    // #region Constructor
    
    app.controller('applicationGroupAddController', applicationGroupAddController);
    applicationGroupAddController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$state', 'commonService'];

    // #endregion

    function applicationGroupAddController($scope, $rootScope, apiService, notificationService, resourceService, $state, commonService) {

        // #region Properties and Variables

        // #region properties
        $scope.applicationGroup = {
            
        }
        //$scope.parentCategories = [];
        $scope.path = 'application_groups';
        $scope.resourcePage = {};
        // #endregion

        // #region methods
        $scope.onAddApplicationGroup = onAddApplicationGroup;
        // #endregion
        
        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });
        
        function onAddApplicationGroup() {
            apiService.post('/api/applicationgroup/add', $scope.applicationGroup
                , function (result) {
                    notificationService.displaySuccess(result.data.Name + ' ' + $scope.resourceShared.Added + '.');
                    $state.go('application_groups');
                }
                , function (error) {
                    notificationService.displayError($scope.resourceShared.AddFail + '.');
                }
            );
        }
        
        function onLoadRoles() {
            apiService.get('/api/applicationRole/getall', null
                , function (result) {
                    $scope.roles = result.data;
                }
                , function (error) {
                    console.log('Cannot get list of parent product categories');
                }
            );
        }

        onLoadRoles();

        // #endregion
    }
   
})(angular.module('tedushop.application_groups'))
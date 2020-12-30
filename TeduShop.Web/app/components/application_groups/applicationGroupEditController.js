
(function (app) {

    // #region Constructor

    app.controller('applicationGroupEditController', applicationGroupEditController);
    applicationGroupEditController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$state', '$stateParams', 'commonService'];

    // #endregion

    function applicationGroupEditController($scope, $rootScope, apiService, notificationService, resourceService, $state, $stateParams, commonService) {

        // #region Properties and Variables
        
        // #region properties
        $scope.applicationGroup = {};
        //$scope.parentCategories = [];
        $scope.path = 'application_groups';
        $scope.resourcePage = {};
        // #endregion

        // #region methods

        $scope.onUpdateApplicationGroup = onUpdateApplicationGroup;

        // #endregion

        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });

        function onLoadApplicationGroupDetail() {
            apiService.get('/api/applicationgroup/getbyid?id=' + $stateParams.id, null 
                , function (result) {
                    $scope.applicationGroup = result.data;
                }
                , function (error) {
                    notificationService.displayError(error.data);
                }
            )
        }
        onLoadApplicationGroupDetail();

        function onUpdateApplicationGroup() {
            apiService.put('/api/applicationgroup/update', $scope.applicationGroup
                , function (result) {
                    console.log(result);
                    notificationService.displaySuccess(result.data.Name + ' ' + $scope.resourceShared.Updated + '.');
                    $state.go('application_groups');
                }
                , function (error) {
                    notificationService.displayError($scope.resourceShared.UpdateFail + '.');
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
    }

})(angular.module('tedushop.application_groups'))
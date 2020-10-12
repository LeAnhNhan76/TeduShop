
(function (app) {

    // #region Constructor

    app.controller('applicationUserEditController', applicationUserEditController);
    applicationUserEditController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService', '$state', '$stateParams', 'commonService'];

    // #endregion

    function applicationUserEditController($scope, $rootScope, apiService, notificationService, resourceService, $state, $stateParams, commonService) {

        // #region Properties and Variables
        
        // #region properties
        $scope.applicationUser = {};
        //$scope.parentCategories = [];
        $scope.path = 'application_users';
        $scope.resourcePage = {};
        // #endregion

        // #region methods

        $scope.onUpdateApplicationUser = onUpdateApplicationUser;

        // #endregion

        // #endregion

        // #region Methods

        //Resource Language
        resourceService.getResourcePage($scope.path, $rootScope.lang).then(function (res) {
            $scope.resourcePage = res;
            $scope.$applyAsync();
        });

        function onLoadApplicationUserDetail() {
            apiService.get('/api/applicationuser/getbyid?id=' + $stateParams.id, null 
                , function (result) {
                    $scope.applicationUser = result.data;
                }
                , function (error) {
                    notificationService.displayError(error.data);
                }
            )
        }
        onLoadApplicationUserDetail();

        function onUpdateApplicationUser() {
            apiService.put('/api/applicationuser/update', $scope.applicationUser
                , function (result) {
                    console.log(result);
                    notificationService.displaySuccess(result.data.Name + ' ' + $scope.resourceShared.Updated + '.');
                    $state.go('application_users');
                }
                , function (error) {
                    notificationService.displayError($scope.resourceShared.UpdateFail + '.');
                }
            );
        }

        // #endregion
    }

})(angular.module('tedushop.application_users'))
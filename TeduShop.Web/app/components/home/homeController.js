(function (app) {
    // #region Constructor

    app.controller('homeController', homeController);
    homeController.$inject = ['$scope', '$rootScope', 'apiService', 'notificationService', 'resourceService'];

    // #endregion

    function homeController($scope, $rootScope, apiService, notificationService, resourceService) {
        // #region Properties and Variables

        // #region properties
        
        // #endregion

        // #region methods
        $scope.onCheckLogin = onCheckLogin;
        // #endregion

        // #endregion

        // #region Methods
        
        function onCheckLogin() {
            apiService.get('/api/home/checklogin', null
                , function (result) {
                    
                }
                , function (error) {
                    console.log('Authentication is failed');
                }
            );
        }
        $scope.onCheckLogin();

        // #endregion
    }
})(angular.module('tedushop.home'));
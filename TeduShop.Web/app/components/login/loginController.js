(function (app) {
    // #region Constructor

    app.controller('loginController', loginController);
    loginController.$inject = ['$scope','$rootScope', 'loginService', '$injector', 'notificationService', 'resourceService'];

    // #endregion

    function loginController($scope, $rootScope, loginService, $injector, notificationService, resourceService) {
        // #region Properties and Variables

        // #region properties
        $scope.loginData = {
            userName: "",
            password: ""
        };
        // #endregion

        // #region methods
        $scope.onLoginSubmit = onLoginSubmit;
        // #endregion

        // #endregion

        // #region Methods
        function onLoginSubmit() {
            loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                if (response != null && response.data.error != undefined) {
                    notificationService.displayError($scope.resourceShared.LoginIncorrect);
                }
                else {
                    notificationService.displaySuccess($scope.resourceShared.LoginIsSuccessfully);
                    var stateService = $injector.get('$state');
                    stateService.go('home');
                    $scope.$applyAsync();
                }
            });
        }
        // #endregion
    }
})(angular.module('tedushop'));
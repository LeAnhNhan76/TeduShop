(function (app) {

    // #region Constructor
    app.controller('baseController', baseController);
    baseController.$inject = ['$scope', '$rootScope', '$state', 'resourceService', 'authDataService', 'loginService', 'authenticationService'];
    // #endregion

    function baseController($scope, $rootScope, $state, resourceService, authDataService, loginService, authenticationService) {
        // #region Properties and Variables
        
        // #region properties
        $scope.resourceShared = {};
        $scope.langArray = [
            { lang: 'vn', titleText1: 'Tiếng Việt', titleText2: 'Vietnamese' },
            { lang: 'en', titleText1: 'Tiếng Anh', titleText2: 'English' }
        ];
        $rootScope.lang = sys.getLanguage().toLowerCase();
        // #endregion

        // #region methods
        $scope.onChangeLanguage = onChangeLanguage;
        $scope.onDisplayTitleLanguage = onDisplayTitleLanguage;
        $scope.onLogout = onLogout;
        //$scope.authentication = authDataService.authenticationData;
        $scope.authentication = authDataService;
        // #endregion
        
        // #endregion

        // #region Methods
        
        function onChangeLanguage (lang) {
            sys.setLanguage(lang);
            $scope.$applyAsync();
            location.reload();
        };

        // Resource Language
        resourceService.getResourceShared($rootScope.lang).then(function (res) {
            $scope.resourceShared = res;
            $scope.$applyAsync();
        });

        function onDisplayTitleLanguage (item) {
            var lang = sys.getLanguage();
            switch (lang) {
                case 'vn':
                    return item.titleText1;
                    break;
                case 'en':
                    return item.titleText2;
                    break;
            }
        }

        function onLogout() {
            loginService.logOut();
            $state.go('login');
        }

        // #endregion
    }
})(angular.module('tedushop'))
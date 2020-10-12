(function (app) {
    'use strict';
    app.service('authenticationService', ['$http', '$q', '$window', 'authDataService',

        function ($http, $q, $window, authDataService) {
            var tokenInfo;

            this.setTokenInfo = function (data) {
                tokenInfo = JSON.stringify(data);
                $window.localStorage['TokenInfo'] = tokenInfo;
            }

            this.getTokenInfo = function () {
                return tokenInfo;
            }

            this.removeToken = function () {
                tokenInfo = null;
                $window.localStorage.removeItem('TokenInfo');
            }

            this.init = function () {
                var tokenInfo = $window.localStorage['TokenInfo'];
                if (tokenInfo) {
                    tokenInfo = JSON.parse(tokenInfo);
                    authDataService.IsAuthenticated = true;
                    authDataService.userName = tokenInfo.userName;
                    authDataService.accessToken = tokenInfo.accessToken;
                }
            }

            this.setHeader = function () {
                delete $http.defaults.headers.common['X-Requested-With'];
                if ((authDataService != undefined) && (authDataService.accessToken != undefined) && (authDataService.accessToken != null) && (authDataService.accessToken != "")) {
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + authDataService.accessToken;
                    $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
                }
            }
            
            this.init();
        }
    ]);
})(angular.module('tedushop.common'));
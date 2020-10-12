(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authDataService','apiService',
        function ($http, $q, authenticationService, authDataService, apiService) {
            var userInfo;
            var deferred;

            this.login = function (userName, password) {
                deferred = $q.defer();
                var data = "grant_type=password&username=" + userName + "&password=" + password;
                $http.post('/oauth/token', data, {
                    headers:
                        { 'Content-Type': 'application/x-www-form-urlencoded' }
                }).then(function (response) {
                    console.log('response', response);
                    userInfo = {
                        accessToken: response.data.access_token,
                        userName: userName
                    };
                    authenticationService.setTokenInfo(userInfo);
                    authDataService.IsAuthenticated = true;
                    authDataService.userName = userName;
                    authDataService.accessToken = userInfo.accessToken;

                    deferred.resolve(null);
                }, function (err) {
                    authDataService.IsAuthenticated = false;
                    authDataService.userName = "";
                    authDataService.accessToken = "";
                    deferred.resolve(err);
                });
                return deferred.promise;
            }

            this.logOut = function () {
                apiService.post('/api/account/logout', null, function (response) {
                    authenticationService.removeToken();
                    delete $http.defaults.headers.common['Authorization'];
                    delete $http.defaults.headers.common['Content-Type'];
                    authDataService.IsAuthenticated = false;
                    authDataService.userName = "";
                    authDataService.accessToken = "";
                    
                }, null);
            }
        }]);
})(angular.module('tedushop.common'));
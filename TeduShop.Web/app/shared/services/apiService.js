/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function (app) {
    // #region Constructor

    app.factory('apiService', apiService);
    apiService.$inject = ['$http', 'notificationService', 'authenticationService'];

    // #endregion

    function apiService($http, notificationService, authenticationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del
        }

        // #region Methods
        
        function get(url, params, success, fail) {
            authenticationService.setHeader();
            $http.get(url, params).then(
                function (result) {
                    success(result);
                }
                , function (error) {
                    fail(error);
                });
        }

        function post(url, params, success, fail) {
            authenticationService.setHeader();
            $http.post(url, params).then(
                function (result) {
                    success(result);
                }, function (error) {
                    if (error.status == 401) {
                        notificationService.displayError('Authentication is required.');
                    }
                    else if (fail != null) {
                        fail(error);
                    }
                });
        }

        function put(url, params, success, fail) {
            authenticationService.setHeader();
            $http.put(url, params).then(
                function (result) {
                    success(result);
                }, function (error) {
                    if (error.status == 401) {
                        notificationService.displayError('Authentication is required.');
                    }
                    else if (fail != null) {
                        fail(error);
                    }
                });
        }

        function del(url, params, success, fail) {
            authenticationService.setHeader();
            $http.delete(url, params).then(
                function (result) {
                    success(result);
                }, function (error) {
                    if (error.status == 401) {
                        notificationService.displayError('Authentication is required.');
                    }
                    else if (fail != null) {
                        fail(error);
                    }
                });
        }

        // #endregion
    }
})(angular.module('tedushop.common'));
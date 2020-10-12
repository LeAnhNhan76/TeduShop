(function (app) {
    'use strict';
    app.factory('authDataService', [function () {
        var authentication = {
            IsAuthenticated: false,
            userName: "",
            accessToken: ""
        };

        return authentication;
    }]);
})(angular.module('tedushop.common'));
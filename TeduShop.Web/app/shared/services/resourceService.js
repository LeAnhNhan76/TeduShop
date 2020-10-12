/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function (app) {
    // #region Constructor

    app.factory('resourceService', resourceService);
    resourceService.$inject = ['$http', '$rootScope'];

    // #endregion

    function resourceService($http, $rootScope) {
        return {
            getResourceShared: getResourceShared,
            getResourcePage: getResourcePage
        }

        // #region Methods

        function getResourceShared(lang) {
            return $.getJSON('/Resources/default/_shared/' + lang + '/common.json', null, function (res) {
                if (res != undefined && res != null) {
                    return res;
                }
                else {
                    return [];
                }
            });
        }
        
        function getResourcePage(path, lang) {
            return $.getJSON('/Resources/default/' + path + '/' + lang + '/common.json', null, function (res) {
                if (res != undefined && res != null) {
                    return res;
                }
                else {
                    return [];
                }
            });
        }

        // #endregion
    }
})(angular.module('tedushop.common'))
/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function () {
    // #region Constructor

    angular.module('tedushop.home', ['tedushop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    // #endregion

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
        .state('home', {
            url: "/home",
            parent: 'base',
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        });
    }
})();
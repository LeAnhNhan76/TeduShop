(function (app) {
    'use strict';

    app.directive('searchEnterDirective', searchEnterDirective);

    function searchEnterDirective() {
        return function (scope, element, attrs) {
            element.bind("keydown keypress", function (event) {
                if (event.which === 13) {
                    scope.$apply(function () {
                        scope.$eval(attrs.searchEnterDirective);
                    });

                    event.preventDefault();
                }
            });
        };
    }

})(angular.module('tedushop.common'));
/// <reference path="../../../assets/admin/libs/angular/angular.js" />

(function (app) {
    // #region Constructor
    app.factory('notificationService', notificationService);

    // #endregion

    function notificationService() {
        // #region Properties and Variables

        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "FadeIn": 300,
            "FadeOut": 1000,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        };

        // #endregion

        // #region Methods

        function displaySuccess(message) {
            toastr.success(message);
        }

        function displayError(error) {
            if (Array.isArray(error)) {
                error.each(function (err) {
                    toastr.error(err);
                });
            }
            else {
                toastr.error(error);
            }
        }

        function displayWarning(message) {
            toastr.warning(message);
        }

        function displayInfo(message) {
            toastr.info(message);
        }

        // #endregion

        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        }
    }
})(angular.module('tedushop.common'));
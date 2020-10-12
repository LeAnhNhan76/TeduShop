var sys;
(function (sys) {
    // #region Properties and Variables

    let _language = "vn";
    var _languages = ["vn", "en"];
    
    // #endregion

    // #region Methods

    sys.setLanguage = function (lang) {
        lang = _languages.includes(lang) ? lang : _language;
        sys.setCookie('TEDUSHOP_LANGUAGE', lang, 7);
    }

    sys.getLanguage = function () {
        var lang = sys.getCookie('TEDUSHOP_LANGUAGE');
        if (lang == null) {
            return _language;
        }
        else {
            return lang;
        }
    }

    sys.changeLanguage = function (lang) {
        this.setLanguage(lang);
        window.location.reload();
    };

    sys.setCookie = function (name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    }

    sys.getCookie = function (name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    sys.eraseCookie = function (name) {
        document.cookie = name + '=; Max-Age=-99999999;';
    }

    // #endregion
})(sys || (sys = {}));
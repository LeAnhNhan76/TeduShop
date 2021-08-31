using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TeduShop.Common.Constants;
using TeduShop.Common.Enums;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Common;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Properties

        protected IErrorService _errorService;

        #endregion Properties

        #region Constructors

        public BaseController(IErrorService errorService)
        {
            this._errorService = errorService;

            #region Language

            HttpCookie langCookie = System.Web.HttpContext.Current.Request.Cookies[Constant.Cookie_Language];
            if (langCookie == null)
            {
                CookieHelper.SetCookieLanguageDefault();
            }

            #endregion Language
        }

        #endregion Constructors

        #region Events

        protected override void Initialize(RequestContext requestContext)
        {
            try
            {
                base.Initialize(requestContext);

                HttpCookie langCookie = Request.Cookies[Constant.Cookie_Language];
                var langCode = langCookie.Value;
                if (!ResourceManagement.languages.Contains(langCode))
                {
                    CookieHelper.SetCookieLanguageDefault();
                    langCode = App.AppLanguageDefault;
                }
                ViewBag.LanguageCurrent = langCode;
                ViewBag.CountInCart = Session[Constant.Session_ShoppingCart] == null ? 0 : (Session[Constant.Session_ShoppingCart] as List<ShoppingCartViewModel>).Count;
                App.DicResources = App.CacheProvider.Get<Dictionary<string, string>>(langCode);
                if (App.DicResources == null)
                {
                    App.DicResources = ResourceManagement.GetResourceByLang(langCode);
                    App.CacheProvider.Set(langCode, AppCachingAbsoluteExpiration.NoneExpiration, () => App.DicResources);
                }

                CultureInfo objCulture = CultureInfo.CreateSpecificCulture(App.Culture);
                objCulture.DateTimeFormat.ShortDatePattern = App.DatePattern;
                objCulture.NumberFormat.NumberDecimalSeparator = ",";
                objCulture.NumberFormat.NumberGroupSeparator = ".";
                Thread.CurrentThread.CurrentCulture = objCulture;
                Thread.CurrentThread.CurrentUICulture = objCulture;
            }
            catch(Exception ex)
            {
                LogError(ex);
            }
        }

        #endregion Events

        #region Methods

        public void LogError(Exception ex)
        {
            try
            {
                var error = new Error()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "log"
                };
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {
            }
        }

        #endregion Methods
    }
}
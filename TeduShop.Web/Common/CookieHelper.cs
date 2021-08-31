using System.Web;
using TeduShop.Common.Constants;

namespace TeduShop.Web.Common
{
    public class CookieHelper
    {
        public static void SetCookieLanguageDefault()
        {
            HttpCookie _langCookie = new HttpCookie(Constant.Cookie_Language);
            _langCookie.Value = App.AppLanguageDefault;
            HttpContext.Current.Response.Cookies.Add(_langCookie);
        }
    }
}
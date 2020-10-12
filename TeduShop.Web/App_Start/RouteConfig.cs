using System.Web.Mvc;
using System.Web.Routing;
using TeduShop.Common.Constants;

namespace TeduShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap.html",
                defaults: new { controller = Constant.Controller_Account, action = Constant.Action_Login, id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Register",
                url: "dang-ky.html",
                defaults: new { controller = Constant.Controller_Account, action = Constant.Action_Register, id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "ShoppingCart",
                url: "gio-hang.html",
                defaults: new { controller = Constant.Controller_ShoppingCart, action = Constant.Action_Index, id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "CheckOut",
                url: "thanh-toan.html",
                defaults: new { controller = Constant.Controller_ShoppingCart, action = Constant.Action_CheckOut, id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "lien-he.html",
                defaults: new { controller = Constant.Controller_Contact, action = Constant.Action_Index, id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "gioi-thieu.html",
                defaults: new { controller = Constant.Controller_About, action = Constant.Action_Index, id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Search",
                url: "tim-kiem.html",
                defaults: new { controller = Constant.Controller_Product, action = Constant.Action_Search, id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Page",
                url: "trang/{alias}.html",
                defaults: new { controller = Constant.Controller_Page, action = Constant.Action_Index, alias = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "TagList",
                url: "tag/{tagId}.html",
                defaults: new { controller = Constant.Controller_Product, action = Constant.Action_GetListByTag, tagId = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Product Category",
                url: "{alias}.pc-{id}.html",
                defaults: new { controller = Constant.Controller_Product, action = Constant.Action_Category, id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Product",
               url: "{alias}.p-{id}.html",
               defaults: new { controller = Constant.Controller_Product, action = Constant.Action_Detail, id = UrlParameter.Optional },
               namespaces: new string[] { "TeduShop.Web.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = Constant.Controller_Home, action = Constant.Action_Index, id = UrlParameter.Optional },
                namespaces: new string[] { "TeduShop.Web.Controllers" }
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TeduShop.Common.Constants;
using TeduShop.Web.Mappings;
using TeduShop.Web.Utilities;

namespace TeduShop.Web
{
    public class App : System.Web.HttpApplication
    {
        #region Variables and Properties

        /// <summary>
        /// PageSize
        /// </summary>
        public static int PageSize = 4;

        /// <summary>
        /// en-EN
        /// </summary>
        public const string Culture = "en-EN";

        /// <summary>
        /// en-EN
        /// </summary>
        public const string AppLanguageDefault = "vn";

        /// <summary>
        /// dd/MM/yyyy
        /// </summary>
        public const string DatePattern = "dd/MM/yyyy";

        /// <summary>
        /// Resource
        /// </summary>
        public static Dictionary<string, string> DicResources = new Dictionary<string, string>();

        /// <summary>
        /// Cache provider
        /// </summary>
        public static CacheService CacheProvider = new CacheService();

        /// <summary>
        /// BIG_SHOPE public identifier
        /// </summary>
        public static string Public_ClientId = string.IsNullOrEmpty(ConfigurationManager.AppSettings[Constant.Public_ClientId].ToString()) ? String.Empty : ConfigurationManager.AppSettings[Constant.Public_ClientId].ToString().ToLower();

        /// <summary>
        /// BIG_SHOPE public secret key
        /// </summary>
        public static string Public_ClientSecret = string.IsNullOrEmpty(ConfigurationManager.AppSettings[Constant.Public_ClientSecret].ToString()) ? String.Empty : ConfigurationManager.AppSettings[Constant.Public_ClientSecret].ToString().ToLower();

        #endregion Variables and Properties

        #region Methods

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapper.Mapper.Initialize(x => { x.AddProfile<AutoMapperConfiguration>(); });
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        #endregion Methods
    }
}
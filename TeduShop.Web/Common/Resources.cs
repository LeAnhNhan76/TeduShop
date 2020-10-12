using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TeduShop.Common.Constants;
using TeduShop.Web;
using TeduShop.Web.Controllers;
using TeduShop.Web.Models;

namespace TeduShop.Web.Common
{
    public class ResourceManagement
    {
        #region Properties and Variables
        public static string[] modules = new string[] { Constant.LanguageModule_Shared, Constant.LanguageModule_Products, Constant.LanguageModule_ProductCategory, Constant.LanguageModule_ApplicationGroup };
        public static string[] languages = new string[] { Constant.Language_VN, Constant.Language_EN};
        public static Dictionary<string, ResourceViewModel> selectListLang = new Dictionary<string, ResourceViewModel>();

        #endregion

        #region Contructors
        static ResourceManagement()
        {
            selectListLang.Add(Constant.Language_VN, new ResourceViewModel() { ResourceText0 = Constant.Language_TiengViet, ResourceText1 = Constant.Language_TiengAnh });
            selectListLang.Add(Constant.Language_EN, new ResourceViewModel() { ResourceText0 = Constant.Language_Vietnamese, ResourceText1 = Constant.Language_English });
        }
        #endregion

        #region Methods

        public static Dictionary<string, string> GetResourceByLang(string lang)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                string path = string.Empty;
                foreach (var module in modules)
                {
                    path = string.Empty;
                    path = System.Web.HttpContext.Current.Server.MapPath(string.Concat(Constant.Path_ResourceDefault, "/", module, "/", lang, "/common.json"));
                    var json = System.IO.File.ReadAllText(path);
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    if(dict.Count > 0)
                    {
                        foreach(var item in dict)
                        {
                            if (!result.Any(x => x.Key == item.Key))
                            {
                                result.Add(item.Key, item.Value);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                DependencyResolver.Current.GetService<BaseController>().LogError(ex);
                result = new Dictionary<string, string>();
            }
            return result;
        }

        public static string GetResourceText(string resourceID)
        {
            try
            {
                return string.IsNullOrEmpty(App.DicResources[resourceID]) ? string.Empty : App.DicResources[resourceID];
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }

        public static string DisplayResourceText(string lang, ResourceViewModel resource)
        {
            string result = string.Empty;
            switch (lang)
            {
                case Constant.Language_VN:
                    result = resource.ResourceText0;
                    break;
                case Constant.Language_EN:
                    result = resource.ResourceText1;
                    break;
                default:
                    break;
            }
            return result;
        }

        #endregion
    }
}

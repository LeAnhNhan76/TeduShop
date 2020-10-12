using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Common.Utilities
{
    public class ConfigHelperUtility
    {
        public static string GetByKey(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
    }
}

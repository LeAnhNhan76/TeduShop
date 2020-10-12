using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Common.Enums
{
    public sealed class AppValue
    {
        public enum SortProduct : byte
        {
            New = 0,
            Popular = 1,
            Discount = 2,
            Price = 3
        }
    }
}

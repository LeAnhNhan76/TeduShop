using System.Collections;
using System.Collections.Generic;

namespace TeduShop.Web.Models.Extensions
{
    public class HomeViewModel
    {
        public List<SlideViewModel> Slides { get; set; }
        public List<ProductViewModel> LatestProducts { get; set; }
        public List<ProductViewModel> TopSaleProducts { get; set; }
    }
}
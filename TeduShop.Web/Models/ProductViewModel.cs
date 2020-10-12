using System;

namespace TeduShop.Web.Models
{
    public class ProductViewModel : AuditableViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public int CategoryID { get; set; }

        public virtual ProductCategoryViewModel ProductCategory { get; set; }

        public string Image { get; set; }

        public string MoreImage { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal? Promotion { get; set; }
        public int? Warranty { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }

        public string Tags { get; set; }
    }
}
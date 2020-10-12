using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeduShop.Web.Models
{
    public class ProductCategoryViewModel : AuditableViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Alias { get; set; }

        public string Description { get; set; }

        public int? ParentID { get; set; }
        public int? DisplayOrder { get; set; }

        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public virtual IEnumerable<ProductViewModel> Products { get; set; }
    }
}
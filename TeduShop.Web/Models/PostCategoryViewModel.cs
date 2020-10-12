using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class PostCategoryViewModel : AuditableViewModel
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public string Alias { get; set; }
        
        public string Description { get; set; }

        public int? ParentID { get; set; }
        public int? DisplayOrder { get; set; }
        
        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public virtual IEnumerable<PostViewModel> Posts { get; set; }
    }
}
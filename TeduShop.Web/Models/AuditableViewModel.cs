using System;

namespace TeduShop.Web.Models
{
    public class AuditableViewModel
    {
        public string MetaKeyword { get; set; }

        public string MetaDescription { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public bool Status { get; set; }
    }
}
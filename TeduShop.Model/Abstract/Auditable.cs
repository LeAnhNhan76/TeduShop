using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Abstract
{
    public abstract class Auditable : IAuditable
    {
        [MaxLength(250)]
        public string MetaKeyword { get; set; }

        [MaxLength(250)]
        public string MetaDescription { get; set; }

        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string UpdatedBy { get; set; }

        public bool Status { get; set; }

        public void SetCreatedDate()
        {
            CreatedDate = DateTime.Now;
        }
        public void SetUpdatedDate()
        {
            UpdatedDate = DateTime.Now;
        }
    }
}
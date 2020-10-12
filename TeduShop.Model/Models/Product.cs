using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;
using TeduShop.Model.Abstract;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_Products)]
    public class Product : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [Required]
        [MaxLength(250)]
        public string Alias { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual ProductCategory ProductCategory { get; set; }

        [MaxLength(500)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal? Promotion { get; set; }
        public int? Warranty { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string Content { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }

        public string Tags { get; set; }

        public virtual IEnumerable<ProductTag> ProductTags { get; set; }
    }
}
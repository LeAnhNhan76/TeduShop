using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_ProductTags)]
    public class ProductTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        [Required]
        public string TagID { get; set; }

        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }
    }
}
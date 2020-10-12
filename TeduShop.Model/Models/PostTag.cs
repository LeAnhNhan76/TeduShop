using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_PostTags)]
    public class PostTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int PostID { get; set; }

        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }

        [Column(TypeName = "varchar")]
        [Required]
        [MaxLength(50)]
        public string TagID { get; set; }

        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }
    }
}
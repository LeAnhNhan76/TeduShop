using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;
using TeduShop.Model.Abstract;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_Posts)]
    public class Post : Auditable
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
        public virtual PostCategory PostCategory { get; set; }

        [MaxLength(500)]
        public string Image { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string Content { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }

        public IEnumerable<PostTag> PostTags { get; set; }
    }
}
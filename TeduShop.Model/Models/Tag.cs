using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_Tags)]
    public class Tag
    {
        [Key]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string ID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string Type { get; set; }

        public virtual IEnumerable<PostTag> PostTags { get; set; }
        public virtual IEnumerable<ProductTag> ProductTags { get; set; }
    }
}
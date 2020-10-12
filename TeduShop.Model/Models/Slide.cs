using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_Slides)]
    public class Slide
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(500)]
        [Required]
        public string Image { get; set; }

        [MaxLength(500)]
        [Required]
        public string URL { get; set; }

        public int? DisplayOrder { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
    }
}
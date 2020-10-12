using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_Pages)]
    public class Page
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        [Required]
        public string Alias { set; get; }

        [Required]
        public string Content { get; set; }

        [MaxLength(250)]
        public string MetaKeyword { get; set; }

        [MaxLength(250)]
        public string MetaDescription { get; set; }

        public bool Status { get; set; }
    }
}
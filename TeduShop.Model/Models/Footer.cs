using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_Footers)]
    public class Footer
    {
        [Key]
        public string ID { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
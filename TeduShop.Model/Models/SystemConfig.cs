using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_SystemConfigs)]
    public class SystemConfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        [Required]
        public string Code { get; set; }

        [MaxLength(250)]
        public string ValueString { get; set; }

        public int? ValueInt { get; set; }
    }
}
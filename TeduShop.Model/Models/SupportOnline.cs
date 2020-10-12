using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_SupportOnlines)]
    public class SupportOnline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Department { get; set; }

        [MaxLength(250)]
        public string Skype { get; set; }

        [MaxLength(250)]
        public string Mobile { get; set; }

        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Yahoo { get; set; }

        [MaxLength(250)]
        public string Facebook { get; set; }

        public bool Status { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_VisitorStatistics)]
    public class VisitorStatistic
    {
        [Key]
        public Guid ID { get; set; }

        public DateTime VisitedDate { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        [Required]
        public string IPAddress { get; set; }
    }
}
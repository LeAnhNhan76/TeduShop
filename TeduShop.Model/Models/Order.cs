using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_Orders)]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(250)]
        public string CustomerEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { get; set; }

        [MaxLength(250)]
        public string CustomerMessage { get; set; }

        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string CreatedBy { get; set; }

        [MaxLength(250)]
        public string PaymentMethod { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentStatus { get; set; }

        public bool Status { get; set; }

        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { get; set; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
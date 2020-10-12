using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_OrderDetails)]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_ApplicationGroups)]
    public class ApplicationGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [StringLength(250)]
        public string Name { set; get; }

        [StringLength(250)]
        public string Description { set; get; }
    }
}
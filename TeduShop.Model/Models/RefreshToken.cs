using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_RefreshTokens)]
    public class RefreshToken
    {
        [Key]
        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string ID { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        [Required]
        public string ClientId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        [Required]
        public string UserName { get; set; }

        public DateTime IssuedTime { get; set; }

        public DateTime ExpiredTime { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        [Required]
        public string ProtectedTicket { get; set; }
    }
}
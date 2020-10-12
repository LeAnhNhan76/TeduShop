using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Common.Constants;

namespace TeduShop.Model.Models
{
    [Table(Constant.Table_Clients)]
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        [Required]
        public string ClientId { set; get; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        [Required]
        public string ClientSecret { set; get; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        [Required]
        public string ClientName { set; get; }
        
        public DateTime CreatedDate { set; get; }

        public int RefreshTokenLifeTime { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(500)]
        [Required]
        public string AllowedOrigin { get; set; }

        public bool IsActive { get; set; }
    }
}
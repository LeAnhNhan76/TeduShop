using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeduShop.Web.Models
{
    public class PostTagViewModel
    {
        public int ID { get; set; }
        
        public int PostID { get; set; }
        
        public virtual PostViewModel Post { get; set; }
        
        public string TagID { get; set; }
        
        public virtual TagViewModel Tag { get; set; }
    }
}
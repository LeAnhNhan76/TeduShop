namespace TeduShop.Web.Models
{
    public class ProductTagViewModel
    {
        public int ID { get; set; }

        public int PostID { get; set; }

        public virtual PostViewModel Post { get; set; }

        public string TagID { get; set; }

        public virtual TagViewModel Tag { get; set; }
    }
}
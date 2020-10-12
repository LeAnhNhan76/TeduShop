using AutoMapper;
using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Mappings
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            #region Map Entity && View Model

            CreateMap<PostCategory, PostCategoryViewModel>().ReverseMap();
            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<Tag, TagViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductTag, ProductTagViewModel>().ReverseMap();
            CreateMap<Footer, FooterViewModel>().ReverseMap();
            CreateMap<Slide, SlideViewModel>().ReverseMap();
            CreateMap<Page, PageViewModel>().ReverseMap();
            CreateMap<ContactDetail, ContactDetailViewModel>().ReverseMap();
            CreateMap<Feedback, FeedbackViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailViewModel>().ReverseMap();
            CreateMap<ApplicationGroup, ApplicationGroupViewModel>().ReverseMap();
            CreateMap<ApplicationRole, ApplicationRoleViewModel>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();

            #endregion Map Entity && View Model
        }
    }
}
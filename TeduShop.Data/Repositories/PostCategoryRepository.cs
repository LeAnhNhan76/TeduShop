using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IPostCategoryRepository : IRepository<PostCategory>
    {
    }

    #endregion Interface

    #region Implement

    public class PostCategoryRepository : RepositoryBase<PostCategory>, IPostCategoryRepository
    {
        #region Constructors
        public PostCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion

        #region Methods
        #endregion

    }

    #endregion Implement
}
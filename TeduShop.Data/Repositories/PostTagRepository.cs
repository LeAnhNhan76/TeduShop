using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IPostTagRepository : IRepository<PostTag>
    {
    }

    #endregion Interface

    #region Implement

    public class PostTagRepository : RepositoryBase<PostTag>, IPostTagRepository
    {
        #region Constructors
        public PostTagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion

        #region Methods
        #endregion

    }

    #endregion Implement
}
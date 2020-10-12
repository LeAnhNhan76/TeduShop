using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface ITagRepository : IRepository<Tag>
    {
    }

    #endregion Interface

    #region Implement

    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        #region Constructors
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion

        #region Methods
        #endregion

    }

    #endregion Implement
}
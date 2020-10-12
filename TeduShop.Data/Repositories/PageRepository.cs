using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IPageRepository : IRepository<Page>
    {
    }

    #endregion Interface

    #region Implement

    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        #region Constructors
        public PageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion

        #region Methods
        #endregion

    }

    #endregion Implement
}
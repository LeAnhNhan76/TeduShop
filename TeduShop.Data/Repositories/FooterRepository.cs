using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IFooterRepository : IRepository<Footer>
    {
    }

    #endregion Interface

    #region Implement

    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        #region Constructors
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion
        
        #region Methods
        #endregion

    }

    #endregion Implement
}
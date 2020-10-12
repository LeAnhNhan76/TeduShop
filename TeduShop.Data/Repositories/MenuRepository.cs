using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IMenuRepository : IRepository<Menu>
    {
    }

    #endregion Interface

    #region Implement

    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        #region Constructors
        public MenuRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion

        #region Methods
        #endregion

    }

    #endregion Implement
}
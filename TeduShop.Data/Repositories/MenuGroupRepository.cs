using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IMenuGroupRepository : IRepository<MenuGroup>
    {
    }

    #endregion Interface

    #region Implement

    public class MenuGroupRepository : RepositoryBase<MenuGroup>, IMenuGroupRepository
    {
        #region Constructors
        public MenuGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion

        #region Methods
        #endregion

    }

    #endregion Implement
}
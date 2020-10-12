using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IApplicationUserGroupRepository : IRepository<ApplicationUserGroup>
    {
    }

    #endregion Interface

    #region Implement

    public class ApplicationUserGroupRepository : RepositoryBase<ApplicationUserGroup>, IApplicationUserGroupRepository
    {
        #region Constructors

        public ApplicationUserGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #endregion Constructors
    }

    #endregion Implement
}
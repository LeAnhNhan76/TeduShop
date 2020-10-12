using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IApplicationRoleGroupRepository : IRepository<ApplicationRoleGroup>
    {
    }

    #endregion Interface

    #region Implement

    public class ApplicationRoleGroupRepository : RepositoryBase<ApplicationRoleGroup>, IApplicationRoleGroupRepository
    {
        #region Constructors

        public ApplicationRoleGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #endregion Constructors
    }

    #endregion Implement
}
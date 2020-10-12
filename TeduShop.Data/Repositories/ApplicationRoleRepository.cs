using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models; 

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId);
    }

    #endregion Interface

    #region Implement

    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        #region Constructors

        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<ApplicationRole> GetListRoleByGroupId(int groupId)
        {
            var query = from g in DbContext.ApplicationRoles
                        join ug in DbContext.ApplicationRoleGroups
                        on g.Id equals ug.RoleId
                        where ug.GroupId == groupId
                        select g;
            return query;
        }

        #endregion Methods

    }

    #endregion Implement
}
using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IApplicationGroupRepository : IRepository<ApplicationGroup>
    {
        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);

        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);
    }

    #endregion Interface

    #region Implement

    public class ApplicationGroupRepository : RepositoryBase<ApplicationGroup>, IApplicationGroupRepository
    {
        #region Constructors

        public ApplicationGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupId
                        where ug.UserId == userId
                        select g;
            return query;
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            var query = from g in DbContext.ApplicationGroups
                        join ug in DbContext.ApplicationUserGroups
                        on g.ID equals ug.GroupId
                        join u in DbContext.Users
                        on ug.UserId equals u.Id
                        where ug.GroupId == groupId
                        select u;
            return query;
        }

        #endregion Methods
    }

    #endregion Implement
}
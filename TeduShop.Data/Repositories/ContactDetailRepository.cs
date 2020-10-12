using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IContactDetailRepository : IRepository<ContactDetail>
    {
    }

    #endregion Interface

    #region Implement

    public class ContactDetailRepository : RepositoryBase<ContactDetail>, IContactDetailRepository
    {
        #region Constructors

        public ContactDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #endregion Constructors
    }

    #endregion Implement
}
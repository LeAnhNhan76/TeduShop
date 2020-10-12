using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IErrorRepository : IRepository<Error>
    {
    }

    #endregion Interface

    #region Implement

    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        #region Constructors
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion

        #region Methods
        #endregion
    }

    #endregion Implement
}
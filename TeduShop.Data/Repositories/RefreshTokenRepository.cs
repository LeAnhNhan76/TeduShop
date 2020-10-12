using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
    }

    #endregion Interface

    #region Implement

    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        #region Constructors

        public RefreshTokenRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #endregion Constructors
    }

    #endregion Implement
}
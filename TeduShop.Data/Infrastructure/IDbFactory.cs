using System;

namespace TeduShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Methods

        TeduShopDbContext Init();

        #endregion
    }
}
namespace TeduShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        #region Properties

        private TeduShopDbContext dbContext;

        #endregion Properties

        #region Constructors

        public TeduShopDbContext Init()
        {
            return dbContext ?? (dbContext = new TeduShopDbContext());
        }

        #endregion Constructors

        #region Methods

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        #endregion Methods
    }
}
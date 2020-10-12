namespace TeduShop.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties

        private readonly IDbFactory dbFactory;
        private TeduShopDbContext dbContext;

        public TeduShopDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        #endregion Properties

        #region Constructors

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        #endregion Constructors

        #region Methods

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        #endregion Methods
    }
}
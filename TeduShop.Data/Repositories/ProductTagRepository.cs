using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IProductTagRepository : IRepository<ProductTag>
    {
    }

    #endregion Interface

    #region Implement

    public class ProductTagRepository : RepositoryBase<ProductTag>, IProductTagRepository
    {
        #region Constructors
        public ProductTagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion

        #region Methods
        #endregion

    }

    #endregion Implement
}
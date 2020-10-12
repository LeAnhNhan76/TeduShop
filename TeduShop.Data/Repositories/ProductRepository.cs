using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow);
    }

    #endregion Interface

    #region Implement

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        #region Constructors

        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        where pt.TagID == tagId
                        select p;

            totalRow = query.Count();

            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        #endregion Methods
    }

    #endregion Implement
}
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IOrderRepository : IRepository<Order>
    {
    }

    #endregion Interface

    #region Implement

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        #region Constructors
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion

        #region Methods
        #endregion

    }

    #endregion Implement
}
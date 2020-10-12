using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
    }

    #endregion Interface

    #region Implement

    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {

        #region Constructors
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        #endregion

        #region Methods
        #endregion

    }

    #endregion Implement
}
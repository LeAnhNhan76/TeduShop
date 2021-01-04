using System.Collections.Generic;
using System.Data.SqlClient;
using TeduShop.Common.ViewModels;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    #region Interface

    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<RevenuesStatisticViewModel> GetRevenuesStatistic(string fromDate, string toDate);
    }

    #endregion Interface

    #region Implement

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        #region Constructors

        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        #endregion Constructors

        #region Methods

        public IEnumerable<RevenuesStatisticViewModel> GetRevenuesStatistic(string fromDate, string toDate)
        {
            var parameters = new object[]
            {
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate)
            };

            return DbContext.Database.SqlQuery<RevenuesStatisticViewModel>("GetRevenuesStatisticSP @fromDate, @toDate", parameters);
        }

        #endregion Methods
    }

    #endregion Implement
}
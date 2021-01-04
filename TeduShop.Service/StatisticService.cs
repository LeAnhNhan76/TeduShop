using System.Collections.Generic;
using TeduShop.Common.ViewModels;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;

namespace TeduShop.Service
{
    public interface IStatisticService
    {
        IEnumerable<RevenuesStatisticViewModel> GetRevenuesStatistic(string fromDate, string toDate);
    }

    public class StatisticService : IStatisticService
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;

        public StatisticService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<RevenuesStatisticViewModel> GetRevenuesStatistic(string fromDate, string toDate)
        {
            return _orderRepository.GetRevenuesStatistic(fromDate, toDate);
        }
    }
}
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;

namespace TeduShop.Web.Api
{
    [RoutePrefix(Api_Statistic)]
    public class StatisticController : BaseApiController
    {
        #region Properties

        private IStatisticService _statisticService;

        #endregion Properties

        #region Constructors

        public StatisticController(IErrorService errorService, IClientService clientService, IStatisticService statisticService) : base(errorService, clientService)
        {
            _statisticService = statisticService;
        }

        #endregion Constructors

        #region Methods

        [Route(Route_GetRevenue)]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string fromDate, string toDate)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetRevenuesStatistic(fromDate, toDate);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }

        #endregion Methods
    }
}
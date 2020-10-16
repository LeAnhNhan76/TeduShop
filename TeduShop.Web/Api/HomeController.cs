using System.Web.Http;
using TeduShop.Common.Constants;
using TeduShop.Service;
using TeduShop.Web.App_Start.Authorize;
using TeduShop.Web.Infrastructure.Core;

namespace TeduShop.Web.Api
{
    [RoutePrefix(Api_Home)]
    [APIAuthorize]
    public class HomeController : BaseApiController
    {
        #region Constructors

        public HomeController(IErrorService errorService, IClientService clientService) : base(errorService, clientService)
        {
        }

        #endregion Constructors

        #region Methods

        [HttpGet]
        [Route(Route_CheckLogin)]
        public string CheckLogin()
        {
            return Constant.HelloTEDUMember;
        }
        
        #endregion Methods
    }
}

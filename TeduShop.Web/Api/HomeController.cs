using System.Web.Http;
using TeduShop.Common.Constants;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;

namespace TeduShop.Web.Api
{
    [RoutePrefix(Api_Home)]
    [Authorize]
    public class HomeController : BaseApiController
    {

        #region Properties
        IErrorService _errorService;
        #endregion Properties

        #region Constructors

        public HomeController(IErrorService errorService) : base(errorService)
        {
            this._errorService = errorService;
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

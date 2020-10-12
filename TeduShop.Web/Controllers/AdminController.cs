using System.Web.Mvc;
using TeduShop.Service;

namespace TeduShop.Web.Controllers
{
    public class AdminController : BaseController
    {
        #region Variables and Properties

        #endregion Variables and Properties

        #region Constructors

        public AdminController(IErrorService errorService) : base(errorService)
        {
        }

        #endregion Constructors

        #region Methods

        public ActionResult Index()
        {
            return View();
        }

        #endregion Methods
    }
}
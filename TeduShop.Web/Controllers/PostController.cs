using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Service;

namespace TeduShop.Web.Controllers
{
    public class PostController : BaseController
    {
        #region Variables and Properties

        private IErrorService _errorService;

        #endregion Variables and Properties

        #region Constructors

        public PostController(IErrorService errorService) : base(errorService)
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
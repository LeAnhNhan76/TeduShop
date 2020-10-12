using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Service;

namespace TeduShop.Web.Controllers
{
    public class AboutController : BaseController
    {
        #region Variables and Properties

        #endregion Variables and Properties

        #region Constructors

        public AboutController(IErrorService errorService) : base(errorService)
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
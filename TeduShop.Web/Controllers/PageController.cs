using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class PageController : BaseController
    {
        #region Variables and Properties
        IPageService _pageService;
        #endregion Variables and Properties

        #region Constructors

        public PageController(IErrorService errorService, IPageService pageService) : base(errorService)
        {
            this._pageService = pageService;
        }

        #endregion Constructors

        #region Methods

        public ActionResult Index(string alias)
        {
            var page = _pageService.GetByAlias(alias);
            var pageVM = Mapper.Map<PageViewModel>(page);
            return View(pageVM);
        }

        #endregion Methods
    }
}
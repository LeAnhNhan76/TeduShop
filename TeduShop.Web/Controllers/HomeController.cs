using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TeduShop.Common.Constants;
using TeduShop.Common.Utilities;
using TeduShop.Service;
using TeduShop.Web.Models;
using TeduShop.Web.Models.Extensions;

namespace TeduShop.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Variables and Properties

        private IProductCategoryService _productCategoryService;
        private IProductService _productService;
        private ICommonService _commonService;

        #endregion Variables and Properties

        #region Constructors

        public HomeController(IProductCategoryService productCategoryService
            , IProductService productService
            , ICommonService commonService
            , IErrorService errorService
            ):base(errorService)
        {
            this._productCategoryService = productCategoryService;
            this._productService = productService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Methods
        
        //[OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var slide = _commonService.GetSlide().ToList();
            var slideVM = Mapper.Map<List<SlideViewModel>>(slide);

            var numLatestProduct = int.Parse(ConfigHelperUtility.GetByKey(Constant.AppSetting_NumLatestProduct));
            var numTopSaleProduct = int.Parse(ConfigHelperUtility.GetByKey(Constant.AppSetting_NumTopSaleProduct));

            var latestProducts = _productService.GetLatest(numLatestProduct).ToList();
            var latestProductsVM = Mapper.Map<List<ProductViewModel>>(latestProducts);

            var topSaleProducts = _productService.GetHotProduct(numTopSaleProduct).ToList();
            var topSaleProductsVM = Mapper.Map<List<ProductViewModel>>(topSaleProducts);

            var model = new HomeViewModel()
            {
                Slides = slideVM,
                LatestProducts = latestProductsVM,
                TopSaleProducts = topSaleProductsVM
            };
            return View(model);
        }
        
        [ChildActionOnly]
        //[OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            var footer = _commonService.GetFooter();
            var model = Mapper.Map<FooterViewModel>(footer);
            return PartialView(Constant.PartialView_FooterPartial, model);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView(Constant.PartialView_HeaderPartial);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600)]
        public ActionResult Category()
        {
            var productCategory = _productCategoryService.GetAll();
            var model = Mapper.Map<List<ProductCategoryViewModel>>(productCategory);
            return PartialView(Constant.PartialView_CategoryPartial, model);
        }

        #endregion Methods
    }
}
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TeduShop.Common.Constants;
using TeduShop.Common.Enums;
using TeduShop.Common.Utilities;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ProductController : BaseController
    {
        #region Variables and Properties

        private IProductCategoryService _productCategoryService;
        private IProductService _productService;
        private ICommonService _commonService;

        #endregion Variables and Properties

        #region Constructors

        public ProductController(IProductCategoryService productCategoryService
            , IProductService productService
            , ICommonService commonService
            , IErrorService errorService
            ) :base(errorService)
        {
            this._productCategoryService = productCategoryService;
            this._productService = productService;
            this._commonService = commonService;
        }

        #endregion Constructors

        #region Methods

        public ActionResult Detail(int id)
        {
            var product = _productService.GetById(id);
            var productVM = Mapper.Map<ProductViewModel>(product);

            var topRelatedProduct = int.Parse(ConfigHelperUtility.GetByKey(Constant.AppSetting_TopRelatedProduct));
            var relatedProducts = _productService.GetRelatedProducts(product.ID, topRelatedProduct);
            ViewBag.RelatedProducts = Mapper.Map<List<ProductViewModel>>(relatedProducts);

            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(productVM.MoreImage);
            ViewBag.MoreImages = listImages;

            var tags = _productService.GetListTagByProductId(product.ID);
            ViewBag.Tags = Mapper.Map<IEnumerable<TagViewModel>>(tags);

            return View(productVM);
        }

        public ActionResult Search(string keyword, int page = 1, AppValue.SortProduct sort = AppValue.SortProduct.New)
        {
            int pageSize = int.Parse(ConfigHelperUtility.GetByKey(Constant.AppSetting_PageSize));
            int totalRow = 0;
            var product = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            var productVM = Mapper.Map<List<ProductViewModel>>(product);
            int totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;

            ViewBag.Keyword = keyword;
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productVM,
                MaxPage = int.Parse(ConfigHelperUtility.GetByKey(Constant.AppSetting_MaxPage)),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        public ActionResult Category(int id, int page = 1, AppValue.SortProduct sort = AppValue.SortProduct.New)
        {
            int pageSize = int.Parse(ConfigHelperUtility.GetByKey(Constant.AppSetting_PageSize));
            int totalRow = 0;
            var product = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            var productVM = Mapper.Map<List<ProductViewModel>>(product);
            int totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;

            var category = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategoryViewModel>(category);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productVM,
                MaxPage = int.Parse(ConfigHelperUtility.GetByKey(Constant.AppSetting_MaxPage)),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var product = _productService.GetListProductByName(keyword).ToList();
            var listProductName = product.Select(x => x.Name).ToList();
            return Json(new {
                data = listProductName
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListByTag(string tagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelperUtility.GetByKey(Constant.AppSetting_PageSize));
            int totalRow = 0;
            var product = _productService.GetListProductByTag(tagId, page, pageSize, out totalRow);
            var productVM = Mapper.Map<List<ProductViewModel>>(product);
            int totalPage = (totalRow % pageSize == 0) ? (totalRow / pageSize) : (totalRow / pageSize) + 1;

            var tag = _productService.GetTagById(tagId);
            ViewBag.Tag = Mapper.Map<TagViewModel>(tag);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productVM,
                MaxPage = int.Parse(ConfigHelperUtility.GetByKey(Constant.AppSetting_MaxPage)),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        #endregion Methods
    }
}
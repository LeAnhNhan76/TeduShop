using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix(Api_ProductCategory)]
    [Authorize]
    public class ProductCategoryController : BaseApiController
    {
        #region Properties

        private IProductCategoryService _productCategoryService;

        #endregion Properties

        #region Constructors

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            _productCategoryService = productCategoryService;
        }

        #endregion Constructors

        #region Methods

        [Route(Route_GetAll)]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var listProductCategory = _productCategoryService.GetAll().ToList();

                var listProductCategoryVM = Mapper.Map<List<ProductCategoryViewModel>>(listProductCategory);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listProductCategoryVM);
                return response;
            });
        }

        [Route(Route_GetPaging)]
        [HttpGet]
        public HttpResponseMessage GetPaging(HttpRequestMessage request, string keyword, int page)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listProductCategory = _productCategoryService.GetAll(keyword).ToList();

                totalRow = listProductCategory.Count();

                var query = listProductCategory.OrderByDescending(p => p.CreatedDate)
                    .Skip((page - 1) * App.PageSize).Take(App.PageSize).ToList();

                var listProductCategoryVM = Mapper.Map<List<ProductCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = listProductCategoryVM,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = totalRow % App.PageSize == 0 ? (totalRow / App.PageSize) : (totalRow / App.PageSize) + 1
                };

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route(Route_GetById)]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var productCategory = _productCategoryService.GetById(id);

                var productCategoryVM = Mapper.Map<ProductCategoryViewModel>(productCategory);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, productCategoryVM);
                return response;
            });
        }

        [Route(Route_Add)]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Add(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var productCategory = Mapper.Map<ProductCategory>(productCategoryVM);
                    productCategory.CreatedDate = DateTime.Now;
                    productCategory.CreatedBy = User.Identity.Name;
                    productCategory.UpdatedDate = DateTime.Now;
                    productCategory.UpdatedBy = User.Identity.Name;
                    var _productCategory = _productCategoryService.Add(productCategory);
                    _productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, _productCategory);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        [Route(Route_Update)]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    if (productCategoryVM == null)
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    var productCategory = Mapper.Map<ProductCategory>(productCategoryVM);
                    productCategory.UpdatedDate = DateTime.Now;
                    productCategory.UpdatedBy = User.Identity.Name;
                    _productCategoryService.Update(productCategory);
                    _productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, productCategoryVM);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        [Route(Route_Delete)]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var productCategory = _productCategoryService.Delete(id);
                    _productCategoryService.Save();
                    var productCategoryVM = Mapper.Map<ProductCategoryViewModel>(productCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, productCategoryVM);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        [Route(Route_DeleteMulti)]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string lstId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var ids = new JavaScriptSerializer().Deserialize<List<int>>(lstId);
                    foreach (var id in ids)
                    {
                        _productCategoryService.Delete(id);
                    }
                    _productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        #endregion Methods
    }
}
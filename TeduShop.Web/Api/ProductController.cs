using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Common.Constants;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix(Api_Product)]
    [Authorize]
    public class ProductController : BaseApiController
    {
        #region Properties

        private IProductService _productService;
        //Test sync repo azure devops with github

        #endregion Properties

        #region Constructors

        public ProductController(IErrorService errorService, IClientService clientService, IProductService productService) : base(errorService, clientService)
        {
            _productService = productService;
        }

        #endregion Constructors

        #region Methods

        [Route(Route_GetAll)]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                // abc
                var listProduct = _productService.GetAll().ToList();

                var listProductVM = Mapper.Map<List<ProductViewModel>>(listProduct);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listProductVM);
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
                var listProduct = _productService.GetAll(keyword).ToList();

                totalRow = listProduct.Count();

                var query = listProduct.OrderByDescending(p => p.CreatedDate)
                    .Skip((page - 1) * App.PageSize).Take(App.PageSize).ToList();

                var listProductVM = Mapper.Map<List<ProductViewModel>>(query);

                var paginationSet = new PaginationSet<ProductViewModel>()
                {
                    Items = listProductVM,
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
                var product = _productService.GetById(id);

                var productVM = Mapper.Map<ProductViewModel>(product);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, productVM);
                return response;
            });
        }

        [Route(Route_Add)]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Add(HttpRequestMessage request, ProductViewModel productVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var product = Mapper.Map<Product>(productVM);
                    product.SetCreatedDate();
                    product.SetUpdatedDate();
                    product.CreatedBy = User.Identity.Name;
                    product.UpdatedBy = User.Identity.Name;
                    var _product = _productService.Add(product);
                    _productService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, _product);
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
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    if (productVM == null)
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    var product = Mapper.Map<Product>(productVM);
                    product.UpdatedDate = DateTime.Now;
                    product.UpdatedBy = User.Identity.Name;
                    _productService.Update(product);
                    _productService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, productVM);
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
                    var product = _productService.Delete(id);
                    _productService.Save();
                    var productVM = Mapper.Map<ProductViewModel>(product);
                    response = request.CreateResponse(HttpStatusCode.Created, productVM);
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
                        _productService.Delete(id);
                    }
                    _productService.Save();
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
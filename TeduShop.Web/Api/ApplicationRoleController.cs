using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Common.Constants;
using TeduShop.Common.Exceptions;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.App_Start;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix(Api_ApplicationRole)]
    [Authorize]
    public class ApplicationRoleController : BaseApiController
    {
        #region Properties

        private IApplicationRoleService _appRoleService;

        #endregion Properties

        #region Constructors

        public ApplicationRoleController(IErrorService errorService
            , IApplicationRoleService appRoleService) : base(errorService)
        {
            _appRoleService = appRoleService;
        }

        #endregion Constructors

        #region Methods

        [Route(Route_GetPaging)]
        [HttpGet]
        public HttpResponseMessage GetPaging(HttpRequestMessage request, string keyword, int page)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var appRole = _appRoleService.GetAll(page, App.PageSize, out totalRow, keyword).ToList();
                var appRoleVM = Mapper.Map<List<ApplicationRoleViewModel>>(appRole);

                PaginationSet<ApplicationRoleViewModel> pagedSet = new PaginationSet<ApplicationRoleViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = totalRow % App.PageSize == 0 ? (totalRow / App.PageSize) : (totalRow / App.PageSize) + 1,
                    Items = appRoleVM
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        [Route(Route_GetAll)]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var appRole = _appRoleService.GetAll().ToList();
                var appRoleVM = Mapper.Map<List<ApplicationRoleViewModel>>(appRole);

                response = request.CreateResponse(HttpStatusCode.OK, appRoleVM);

                return response;
            });
        }
        [Route(Route_GetById)]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có giá trị.");
            }
            ApplicationRole appRole = _appRoleService.GetDetail(id);
            if (appRole == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No group");
            }
            return request.CreateResponse(HttpStatusCode.OK, appRole);
        }

        [HttpPost]
        [Route(Route_Add)]
        public HttpResponseMessage Add(HttpRequestMessage request, ApplicationRoleViewModel applicationRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppRole = new ApplicationRole();
                newAppRole.UpdateApplicationRole(applicationRoleViewModel);
                try
                {
                    _appRoleService.Add(newAppRole);
                    _appRoleService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, applicationRoleViewModel);
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route(Route_Update)]
        public HttpResponseMessage Update(HttpRequestMessage request, ApplicationRoleViewModel applicationRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                var appRole = _appRoleService.GetDetail(applicationRoleViewModel.Id);
                try
                {
                    appRole.UpdateApplicationRole(applicationRoleViewModel, Constant.Action_Update);
                    _appRoleService.Update(appRole);
                    _appRoleService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, appRole);
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Route(Route_Delete)]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
        {
            _appRoleService.Delete(id);
            _appRoleService.Save();
            return request.CreateResponse(HttpStatusCode.OK, id);
        }

        [Route(Route_DeleteMulti)]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedList)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listItem = new JavaScriptSerializer().Deserialize<List<string>>(checkedList);
                    foreach (var item in listItem)
                    {
                        _appRoleService.Delete(item);
                    }

                    _appRoleService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }

        #endregion Methods
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Common.Exceptions;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.App_Start;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix(Api_ApplicationGroup)]
    [Authorize]
    public class ApplicationGroupController : BaseApiController
    {
        #region Properties
        
        private IApplicationGroupService _appGroupService;
        private IApplicationRoleService _appRoleService;
        private ApplicationUserManager _userManager;

        #endregion Properties

        #region Constructors

        public ApplicationGroupController(IErrorService errorService,
            IApplicationRoleService appRoleService,
            ApplicationUserManager userManager,
            IApplicationGroupService appGroupService,
            IClientService clientService) : base(errorService, clientService)
        {
            _appGroupService = appGroupService;
            _appRoleService = appRoleService;
            _userManager = userManager;
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
                var appGroup = _appGroupService.GetAll(page, App.PageSize, out totalRow, keyword).ToList();
                var appGroupVM = Mapper.Map<List<ApplicationGroupViewModel>>(appGroup);

                PaginationSet<ApplicationGroupViewModel> pagedSet = new PaginationSet<ApplicationGroupViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = totalRow % App.PageSize == 0 ? (totalRow / App.PageSize) : (totalRow / App.PageSize) + 1,
                    Items = appGroupVM
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
                var appGroup = _appGroupService.GetAll().ToList();
                var appGroupVM = Mapper.Map<List<ApplicationGroupViewModel>>(appGroup);

                response = request.CreateResponse(HttpStatusCode.OK, appGroupVM);

                return response;
            });
        }
        [Route(Route_GetById)]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            if (id == 0)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " is required.");
            }
            ApplicationGroup appGroup = _appGroupService.GetDetail(id);
            var appGroupVM = Mapper.Map<ApplicationGroupViewModel>(appGroup);
            if (appGroup == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No group");
            }
            var listRole = _appRoleService.GetListRoleByGroupId(appGroupVM.ID);
            appGroupVM.Roles = Mapper.Map<IEnumerable<ApplicationRoleViewModel>>(listRole);
            return request.CreateResponse(HttpStatusCode.OK, appGroupVM);
        }

        [HttpPost]
        [Route(Route_Add)]
        public HttpResponseMessage Add(HttpRequestMessage request, ApplicationGroupViewModel appGroupVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var appGroup = Mapper.Map<ApplicationGroup>(appGroupVM);
                    var newAppGroup = _appGroupService.Add(appGroup);
                    _appGroupService.Save();

                    ////save group
                    //var listRoleGroup = new List<ApplicationRoleGroup>();
                    //foreach (var role in appGroupVM.Roles)
                    //{
                    //    listRoleGroup.Add(new ApplicationRoleGroup()
                    //    {
                    //        GroupId = appGroup.ID,
                    //        RoleId = role.Id
                    //    });
                    //}
                    //_appRoleService.AddRolesToGroup(listRoleGroup, appGroup.ID);
                   // _appRoleService.Save();

                    return request.CreateResponse(HttpStatusCode.OK, appGroupVM);
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
        public HttpResponseMessage Update(HttpRequestMessage request, ApplicationGroupViewModel appGroupVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var appGroup = _appGroupService.GetDetail(appGroupVM.ID);
                    appGroup.UpdateApplicationGroup(appGroupVM);
                    _appGroupService.Update(appGroup);
                    _appGroupService.Save();

                    ////save group
                    //var listRoleGroup = new List<ApplicationRoleGroup>();
                    //foreach (var role in appGroupVM.Roles)
                    //{
                    //    listRoleGroup.Add(new ApplicationRoleGroup()
                    //    {
                    //        GroupId = appGroup.ID,
                    //        RoleId = role.Id
                    //    });
                    //}
                    //_appRoleService.AddRolesToGroup(listRoleGroup, appGroup.ID);
                    //_appRoleService.Save();

                    ////add role to user
                    //var listRole = _appRoleService.GetListRoleByGroupId(appGroup.ID);
                    //var listUserInGroup = _appGroupService.GetListUserByGroupId(appGroup.ID);
                    //foreach (var user in listUserInGroup)
                    //{
                    //    var listRoleName = listRole.Select(x => x.Name).ToArray();
                    //    foreach (var roleName in listRoleName)
                    //    {
                    //        await _userManager.RemoveFromRoleAsync(user.Id, roleName);
                    //        await _userManager.AddToRoleAsync(user.Id, roleName);
                    //    }
                    //}
                    return request.CreateResponse(HttpStatusCode.OK, appGroupVM);
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
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var appGroup = _appGroupService.Delete(id);
            _appGroupService.Save();
            return request.CreateResponse(HttpStatusCode.OK, appGroup);
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
                    var listItem = new JavaScriptSerializer().Deserialize<List<int>>(checkedList);
                    foreach (var item in listItem)
                    {
                        _appGroupService.Delete(item);
                    }

                    _appGroupService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }

        #endregion Methods
    }
}

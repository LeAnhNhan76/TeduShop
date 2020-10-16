using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.Web.Infrastructure.Core
{
    public class BaseApiController : ApiController
    {
        #region Properties

        private IErrorService _errorService;
        private readonly IClientService _clientService;

        #endregion Properties

        #region Constructors

        public BaseApiController(IErrorService errorService
            , IClientService clientService)
        {
            this._errorService = errorService;
            this._clientService = clientService;
        }

        #endregion Constructors

        #region Methods

        public HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var item in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{item.Entry.Entity.GetType().Name}\" in state \"{item.Entry.State}\" has the following validation error.");
                    foreach (var ve in item.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        #endregion Methods

        #region Log Error

        private void LogError(Exception ex)
        {
            try
            {
                var error = new Error()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "log"
                };
                _errorService.Create(error);
                _errorService.Save();
            }
            catch
            {
            }
        }

        #endregion Log Error

        #region Contants

        #region Route Prefix

        /// <summary>
        /// api/token
        /// </summary>
        public const string Api_Token = "api/token";

        /// <summary>
        /// api/home
        /// </summary>
        public const string Api_Home = "api/home";

        /// <summary>
        /// api/postcategory
        /// </summary>
        public const string Api_PostCategory = "api/postcategory";

        /// <summary>
        /// api/account
        /// </summary>
        public const string Api_Account = "api/account";

        /// <summary>
        /// api/productcategory
        /// </summary>
        public const string Api_ProductCategory = "api/productcategory";


        /// <summary>
        /// api/product
        /// </summary>
        public const string Api_Product = "api/product";

        /// <summary>
        /// api/applicationgroup
        /// </summary>
        public const string Api_ApplicationGroup = "api/applicationgroup";

        /// <summary>
        /// api/applicationrole
        /// </summary>
        public const string Api_ApplicationRole = "api/applicationrole";

        /// <summary>
        /// api/applicationuser
        /// </summary>
        public const string Api_ApplicationUser = "api/applicationuser";

        #endregion Route Prefix

        #region Route

        /// <summary>
        /// getall
        /// </summary>
        public const string Route_GetAll = "getall";

        /// <summary>
        /// getpaging
        /// </summary>
        public const string Route_GetPaging = "getpaging";

        /// <summary>
        /// getbyid
        /// </summary>
        public const string Route_GetById = "getbyid";

        /// <summary>
        /// add
        /// </summary>
        public const string Route_Add = "add";

        /// <summary>
        /// update
        /// </summary>
        public const string Route_Update = "update";

        /// <summary>
        /// delete
        /// </summary>
        public const string Route_Delete = "delete";

        /// <summary>
        /// deletemulti
        /// </summary>
        public const string Route_DeleteMulti = "deletemulti";

        /// <summary>
        /// login
        /// </summary>
        public const string Route_Login = "login";

        /// <summary>
        /// logout
        /// </summary>
        public const string Route_Logout = "logout";

        /// <summary>
        /// testmethod
        /// </summary>
        public const string Route_CheckLogin = "checklogin";

        #endregion Route

        #endregion Contants
    }
}
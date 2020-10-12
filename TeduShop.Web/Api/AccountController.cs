using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TeduShop.Web.App_Start;
using TeduShop.Web.Infrastructure.Core;

namespace TeduShop.Web.Api
{
    [RoutePrefix(BaseApiController.Api_Account)]
    public class AccountController : ApiController
    {
        #region Properties

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        #endregion Properties

        #region Constructors

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #endregion Constructors

        #region Methods

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager authenticationManager => HttpContext.Current.GetOwinContext().Authentication;

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [Route(BaseApiController.Route_Login)]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request, string userName, string password, bool rememberMe)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null) return request.CreateResponse(HttpStatusCode.NotFound, "Login is failed");
            var checkSignIn = await SignInManager.PasswordSignInAsync(user.UserName, password, rememberMe, shouldLockout: false);
            if(checkSignIn == SignInStatus.Success)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userName));
                claims.Add(new Claim(ClaimTypes.Name, user.FullName));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                var claimsIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(claimsIdentity);
            }

            return request.CreateResponse(HttpStatusCode.OK, "Login is succesfully");
        }

        [HttpPost]
        [Authorize]
        [Route(BaseApiController.Route_Logout)]
        public HttpResponseMessage Logout(HttpRequestMessage request)
        {
            // var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            
            return request.CreateResponse(HttpStatusCode.OK, new { success = true });
        }

        #endregion Methods
    }
}
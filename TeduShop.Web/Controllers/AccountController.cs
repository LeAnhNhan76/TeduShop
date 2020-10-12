using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TeduShop.Common.Constants;
using TeduShop.Common.Utilities;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.App_Start;
using TeduShop.Web.Common;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region Variables and Properties
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        #endregion Variables and Properties

        #region Constructors

        public AccountController(IErrorService errorService
            , ApplicationUserManager userManager
            , ApplicationSignInManager signInManager) : base(errorService)
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
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction(Constant.Action_Index, Constant.Controller_Home);
                    }
                }
                else
                {
                    ModelState.AddModelError("", ResourceManagement.GetResourceText(Constant.Resource_LoginIncorrect));
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction(Constant.Action_Index, Constant.Controller_Home);
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation(Constant.CaptchaCode, Constant.RegisterCaptcha, "Mã xác nhận không đúng")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userByEmail != null)
                {
                    ModelState.AddModelError(Constant.ModelError_Email, ResourceManagement.GetResourceText(Constant.Resource_UserExisted));
                    return View(model);
                }
                var userByUserName = await _userManager.FindByNameAsync(model.UserName);
                if (userByUserName != null)
                {
                    ModelState.AddModelError(Constant.ModelError_Email, ResourceManagement.GetResourceText(Constant.Resource_UserExisted));
                    return View(model);
                }
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    Birthday = DateTime.Now,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address

                };

                await _userManager.CreateAsync(user, model.Password);


                var adminUser = await _userManager.FindByEmailAsync(model.Email);
                if (adminUser != null)
                    await _userManager.AddToRolesAsync(adminUser.Id, new string[] { Constant.Role_User });

                string content = System.IO.File.ReadAllText(Server.MapPath(Constant.Path_NewUserTemplate));
                content = content.Replace("{{UserName}}", adminUser.FullName);
                content = content.Replace("{{Link}}", ConfigHelperUtility.GetByKey(Constant.AppSetting_CurrentLink) + "dang-nhap.html");

                MailHelperUtility.SendMail(adminUser.Email, ResourceManagement.GetResourceText(Constant.Resource_RegisterSuccessfully), content);


                ViewData[Constant.ViewData_SuccessMsg] = ResourceManagement.GetResourceText(Constant.Resource_RegisterSuccessfully);
            }

            return View();
        }

        #endregion Methods
    }
}
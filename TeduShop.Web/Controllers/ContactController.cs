using AutoMapper;
using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Common.Constants;
using TeduShop.Common.Utilities;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Common;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ContactController : BaseController
    {
        #region Variables and Properties
        IContactDetailService _contactDetailService;
        IFeedbackService _feedbackService;
        #endregion Variables and Properties

        #region Constructors

        public ContactController(IErrorService errorService
            , IContactDetailService contactDetailService
            , IFeedbackService feedbackService) : base(errorService)
        {
            this._contactDetailService = contactDetailService;
            this._feedbackService = feedbackService;
        }

        #endregion Constructors

        #region Methods

        public ActionResult Index()
        {
            FeedbackViewModel feedbackVM = new FeedbackViewModel()
            {
                ContactDetail = GetDetail()
            };

            return View(feedbackVM);
        }
        
        [HttpPost]
        [CaptchaValidation(Constant.CaptchaCode, Constant.ContactCaptcha, errorMessage:"Mã xác nhận không đúng")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackVM)
        {
            if (ModelState.IsValid)
            {
                var feedback = Mapper.Map<Feedback>(feedbackVM);
                feedback.CreatedDate = DateTime.Now;
                _feedbackService.Create(feedback);
                _feedbackService.Save();

                ViewData[Constant.ViewData_SuccessMsg] = ResourceManagement.GetResourceText(Constant.Resource_SendFeedbackSuccess);

                string content = System.IO.File.ReadAllText(Server.MapPath(Constant.Path_ContactTemplate));
                content = content.Replace("{{Name}}", feedbackVM.Name);
                content = content.Replace("{{Email}}", feedbackVM.Email);
                content = content.Replace("{{Message}}", feedbackVM.Message);
                var adminEmail = ConfigHelperUtility.GetByKey(Constant.AppSetting_AdminEmail);
                MailHelperUtility.SendMail(adminEmail, ResourceManagement.GetResourceText(Constant.Resource_ContactInformationFromWebsite), content);

                feedbackVM.Name = string.Empty;
                feedbackVM.Message = string.Empty;
                feedbackVM.Email = string.Empty;
            }
            feedbackVM.ContactDetail = GetDetail();

            return View(Constant.Action_Index, feedbackVM);
        }

        private ContactDetailViewModel GetDetail()
        {
            var model = _contactDetailService.GetDefaultContact();
            var contactViewModel = Mapper.Map<ContactDetailViewModel>(model);
            return contactViewModel;
        }

        #endregion Methods
    }
}
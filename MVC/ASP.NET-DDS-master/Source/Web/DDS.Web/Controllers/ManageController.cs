﻿namespace DDS.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Common;
    using DDS.Web.ViewModels.Manage;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Services.Data.Interfaces;

    [Authorize]
    public class ManageController : BaseController
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private readonly IStudentsService students;
        private readonly ITeachersService teachers;
        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;

        public ManageController(IStudentsService students, ITeachersService teachers)
        {
            this.students = students;
            this.teachers = teachers;
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? this.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set
            {
                this.signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager => this.HttpContext.GetOwinContext().Authentication;

        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage = message == ManageMessageId.ChangePasswordSuccess
                                             ? "Паролата Ви е променена."
                                             : message == ManageMessageId.SetPasswordSuccess
                                                   ? "Паролата Ви е зададена."
                                                   : message == ManageMessageId.SetTwoFactorSuccess
                                                         ? "Your two-factor authentication provider has been set."
                                                         : message == ManageMessageId.Error
                                                               ? "Възникна грешка."
                                                               : message == ManageMessageId.AddPhoneSuccess
                                                                     ? "Вашият телефон бе добавен."
                                                                     : message == ManageMessageId.RemovePhoneSuccess
                                                                           ? "Вашия телефон бе изтрит."
                                                                           : string.Empty;

            var userId = this.User.Identity.GetUserId();
            var user = this.UserManager.FindById(userId);

            var model = new IndexViewModel
            {
                UserId = userId,
                HasPassword = this.HasPassword(),
                PhoneNumber = await this.UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await this.UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await this.UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await this.AuthenticationManager.TwoFactorBrowserRememberedAsync(userId),
                ScienceDegree = user.ScienceDegree,
            };

            if (this.UserManager.IsInRole(model.UserId, GlobalConstants.StudentRoleName))
            {
                var student = this.students.GetByUserId(model.UserId).Include(s => s.SelectedDiploma).FirstOrDefault();

                if (student.SelectedDiploma != null)
                {
                    model.DiplomaId = student.SelectedDiploma.Id;
                    this.TempData["HasDiploma"] = true;
                }

                model.Address = student.Address;
                model.FNumber = student.FNumber;
                model.IsStudent = true;
                this.TempData["Student"] = true;
            }
            else
            {
                model.IsStudent = false;
            }

            return this.View(model);
        }

        // POST: /Manage/SaveInfo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveInfo(IndexViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = this.UserManager.FindById(model.UserId);

                user.PhoneNumber = model.PhoneNumber;
                user.ScienceDegree = model.ScienceDegree;
                this.UserManager.Update(user);

                var student = this.students.GetByUserId(model.UserId).Where(s => !s.IsDeleted).FirstOrDefault();
                if (student != null)
                {
                    user.Student = student;
                    user.Student.FNumber = model.FNumber;
                    user.Student.Address = model.Address;
                    this.students.Save();
                }

                return this.RedirectToAction("Index", "Home", null);
            }

            return this.View("Index", model);
        }

        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return this.View();
        }

        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result =
                await
                this.UserManager.ChangePasswordAsync(
                    this.User.Identity.GetUserId(),
                    model.OldPassword,
                    model.NewPassword);
            if (result.Succeeded)
            {
                var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                if (user != null)
                {
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                return this.RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }

            this.AddErrors(result);
            return this.View(model);
        }

        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return this.View();
        }

        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = await this.UserManager.AddPasswordAsync(this.User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await this.UserManager.FindByIdAsync(this.User.Identity.GetUserId());
                    if (user != null)
                    {
                        await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }

                    return this.RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.userManager != null)
            {
                this.userManager.Dispose();
                this.userManager = null;
            }

            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error);
            }
        }

        private bool HasPassword()
        {
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            return user?.PasswordHash != null;
        }

        private bool HasPhoneNumber()
        {
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            return user?.PhoneNumber != null;
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Projekt_ANM.Models;

namespace Projekt_ANM.Controllers
{    
    //Zabezpieczenie przed nieautoryzowanym dostępem
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
            
        }
      

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

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

        // Przekierowania do użytkowników
        public ActionResult ReturnUsers()
        {
            return new RedirectResult(@"~\Users\Index");
        }

        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Twoje hasło zostało zmienione."
                : message == ManageMessageId.SetPasswordSuccess ? "Twoje hasło zozstało ustawione."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Twój dostawca uwierzytelniania dwuskładnikowego został ustawiony."
                : message == ManageMessageId.Error ? "Wystąpił błąd."
                : message == ManageMessageId.AddPhoneSuccess ? "Twój numer telefonu został dodany."
                : message == ManageMessageId.RemovePhoneSuccess ? "Twój numer telefonu został usunięty."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //Statyczna zmienna która przechowuje id użytkownika
        private static string Local;

        // GET: Załadowanie widoku zmiany hasła
        public ActionResult ChangePassword()
        {
            if (TempData["passedit"] != null )
            {
                Local = TempData["passedit"].ToString();

            }         
            return View();
        }

        // POST: Funkcja zmiany hasła
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            TempData["local"] = Local;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string xuser = "";
            if (TempData["local"] != null)
            {
                xuser = TempData["local"].ToString();
            }
            else
            {
                xuser = User.Identity.GetUserId();
            }
            var result = await UserManager.ChangePasswordAsync(xuser, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(xuser);
                if (user != null)
                {
                    //Wyłączone ponieważ:
                    //Użytkownik zawsze musi być zalogowany by wykonać operację zmiany hasła
                    //Administrator po zmianie hasła może logować się na drugie konto
                  //  await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                if (TempData["local"] != null)
                {
                    return RedirectToAction("Index", "Users");

                }
                else
                {
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
            }
            AddErrors(result);
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
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

#endregion
    }
}
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Application.ViewModels;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.UI.Sistema.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public ManageController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "A senha foi alterada."
                    : message == ManageMessageId.SetPasswordSuccess
                        ? "A senha foi enviada."
                        : message == ManageMessageId.SetTwoFactorSuccess
                            ? "A segunda validação foi enviada."
                            : message == ManageMessageId.Error
                                ? "Ocorreu um erro."
                                : message == ManageMessageId.AddPhoneSuccess
                                    ? "O Telefone foi adicionado."
                                    : message == ManageMessageId.RemovePhoneSuccess
                                        ? "O Telefone foi removido."
                                        : "";

            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(User.Identity.GetUserId()),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(User.Identity.GetUserId()),
                Logins = await _userManager.GetLoginsAsync(User.Identity.GetUserId()),
                BrowserRemembered =
                    await AuthenticationManager.TwoFactorBrowserRememberedAsync(User.Identity.GetUserId())
            };
            return View(model);
        }

        public ActionResult RemoveLogin()
        {
            var linkedAccounts = _userManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return View(linkedAccounts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await _userManager.RemoveLoginAsync(User.Identity.GetUserId(),
                new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                    await SignInAsync(user, false);
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new {Message = message});
        }

        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            // Gerar um token e enviar
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (_userManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "O código de segurança é: " + code
                };
                await _userManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new {PhoneNumber = model.Number});
        }

        [HttpPost]
        public ActionResult RememberBrowser()
        {
            var rememberBrowserIdentity =
                AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(User.Identity.GetUserId());
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = true}, rememberBrowserIdentity);
            return RedirectToAction("Index", "Manage");
        }

        [HttpPost]
        public ActionResult ForgetBrowser()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            return RedirectToAction("Index", "Manage");
        }

        [HttpPost]
        public async Task<ActionResult> EnableTfa()
        {
            await _userManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
                await SignInAsync(user, false);
            return RedirectToAction("Index", "Manage");
        }

        [HttpPost]
        public async Task<ActionResult> DisableTfa()
        {
            await _userManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
                await SignInAsync(user, false);
            return RedirectToAction("Index", "Manage");
        }

        public ActionResult VerifyPhoneNumber(string phoneNumber)
        {
            return phoneNumber == null
                ? View("Error")
                : View(new VerifyPhoneNumberViewModel {PhoneNumber = phoneNumber});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result =
                await _userManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                    await SignInAsync(user, false);
                return RedirectToAction("Index", new {Message = ManageMessageId.AddPhoneSuccess});
            }
            // No caso de falha, reexibir a view. 
            ModelState.AddModelError("", "Falha ao adicionar telefone");
            return View(model);
        }

        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await _userManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
                return RedirectToAction("Index", new {Message = ManageMessageId.Error});
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
                await SignInAsync(user, false);
            return RedirectToAction("Index", new {Message = ManageMessageId.RemovePhoneSuccess});
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result =
                await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                    await SignInAsync(user, false);
                return RedirectToAction("Index", new {Message = ManageMessageId.ChangePasswordSuccess});
            }
            AddErrors(result);
            return View(model);
        }

        public ActionResult SetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                    await SignInAsync(user, false);
                return RedirectToAction("Index", new {Message = ManageMessageId.SetPasswordSuccess});
            }
            AddErrors(result);

            // No caso de falha, reexibir a view. 
            return View(model);
        }

        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess
                    ? "O Login foi removido."
                    : message == ManageMessageId.Error
                        ? "Ocorreu um erro."
                        : "";
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
                return View("Error");
            var userLogins = await _userManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes()
                .Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"),
                User.Identity.GetUserId());
        }

        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
                return RedirectToAction("ManageLogins", new {Message = ManageMessageId.Error});
            var result = await _userManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded
                ? RedirectToAction("ManageLogins")
                : RedirectToAction("ManageLogins", new {Message = ManageMessageId.Error});
        }

        
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            var clientKey = Request.Browser.Type;
            await _userManager.SignInClientAsync(user, clientKey);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie,
                DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent},
                await user.GenerateUserIdentityAsync(_userManager));
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);
        }

        private bool HasPassword()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user != null)
                return user.PasswordHash != null;
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user != null)
                return user.PhoneNumber != null;
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
    }
}
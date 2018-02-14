using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Application.ViewModels;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.UI.Sistema.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly ILoginAppService _loginAppService;

        private const string XsrfKey = "XsrfId";
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(ILoginAppService loginAppService, IUsuarioAppService usuarioAppService)
        {
            _loginAppService = loginAppService;
            _usuarioAppService = usuarioAppService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _loginAppService.ObterStatusLogin(model.Email, model.Password, model.RememberMe);

            switch (result)
            {
                case SignInStatus.Success:
                    var user = await _usuarioAppService.ObterUsuario(model.Email, model.Password);
                    if (!user.EmailConfirmed)
                        TempData["AvisoEmail"] = "Usuário não confirmado, verifique seu e-mail.";
                    var clientKey = Request.Browser.Type;
                    await _loginAppService.EfetuarLogin(user, model.RememberMe, AuthenticationManager, clientKey);
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                default:
                    ModelState.AddModelError("", "Login ou Senha incorretos.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, string userId)
        {
            if (!await _loginAppService.UsuarioVerificado())
                return View("Error");

            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, UserId = userId });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _loginAppService.ObterStatusLoginTwoFactor(model.Provider, model.Code, model.RememberBrowser);

            switch (result)
            {
                case SignInStatus.Success:
                    var user = _usuarioAppService.ObterUsuario(model.UserId);
                    await _loginAppService.EfetuarLogin(user.Result, false, AuthenticationManager, Request.Browser.Type);
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                default:
                    ModelState.AddModelError("", "Código Inválido.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Email = model.Email
            };

            var result = await _usuarioAppService.IncluirUsuarioSenha(user, model.Password);

            if (result.Succeeded)
            {
                var code = await _loginAppService.ObterTokenEmail(user.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, Request.Url?.Scheme);
                await _usuarioAppService.EnviarEmail(user.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link: <a href='" + callbackUrl + "'></a>");
                return View("DisplayEmail");
            }

            AddErrors(result);

            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string codUsuario, string codigoVerificacao)
        {
            if (codUsuario == null || codigoVerificacao == null)
                return View("Error");

            var result = await _usuarioAppService.ConfirmarEmail(new Guid(codUsuario), codigoVerificacao);

            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _usuarioAppService.ObterUsuario(model.Email);

            if (user == null || !await _usuarioAppService.EmailConfirmado(new Guid(user.Id)))
                return View("ForgotPasswordConfirmation");

            var code = await _loginAppService.GerarToken(user.Id);

            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, Request.Url?.Scheme);

            await _usuarioAppService.EnviarEmail(user.Id, "Esqueci minha senha", "Por favor altere sua senha clicando aqui: <a href='" + callbackUrl + "'></a>");

            ViewBag.Link = callbackUrl;

            return View("ForgotPasswordConfirmation");
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string codigoSeguranca)
        {
            return codigoSeguranca == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _usuarioAppService.ObterUsuario(model.Email);
            if (user == null)
                return RedirectToAction("ResetPasswordConfirmation", "Account");

            var result = await _loginAppService.ResetarSenha(user.Id, model.Code, model.Password);
            if (result.Succeeded)
                return RedirectToAction("ResetPasswordConfirmation", "Account");

            AddErrors(result);

            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl)
        {
            var userId = await _loginAppService.ObterUsuarioVerificado();

            if (userId == null)
                return View("Error");

            var userFactors = await _loginAppService.ConsultarProvedores(userId);

            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose })
                .ToList();

            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, UserId = userId });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            if (!await _loginAppService.EnviarToken(model.SelectedProvider))
                return View("Error");

            return RedirectToAction("VerifyCode",
                new { Provider = model.SelectedProvider, model.ReturnUrl, userId = model.UserId });
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();

            if (loginInfo == null)
                return RedirectToAction("Login");

            var user = await _usuarioAppService.ObterUsuario(loginInfo.Login);

            // Logar caso haja um login externo e já esteja logado neste provedor de login
            var result = await _loginAppService.EfetuarLoginExterno(loginInfo, false);

            switch (result)
            {
                case SignInStatus.Success:
                    var userext = _usuarioAppService.ObterUsuarioPorEmail(user.Email);
                    await _loginAppService.EfetuarLogin(userext.Result, false, AuthenticationManager, Request.Browser.Type);
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                default:
                    // Se ele nao tem uma conta solicite que crie uma

                    var externalIdentity = HttpContext.GetOwinContext().Authentication
                        .GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
                    var email = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:email")
                        ?.Value;
                    var firstName = externalIdentity.Result.Claims
                        .FirstOrDefault(c => c.Type == "urn:facebook:first_name")?.Value;
                    var lastName = externalIdentity.Result.Claims
                        .FirstOrDefault(c => c.Type == "urn:facebook:last_name")?.Value;

                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation",
                        new ExternalLoginConfirmationViewModel
                        {
                            Email = loginInfo.Email,
                            LastName = lastName,
                            Name = firstName
                        });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Manage");

            if (ModelState.IsValid)
            {
                // Pegar a informação do login externo.
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                    return View("ExternalLoginFailure");
                var usuario = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _usuarioAppService.IncluirUsuario(usuario);
                if (result.Succeeded)
                {
                    result = await _loginAppService.AdicionarLogin(usuario.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await _loginAppService.EfetuarLogin(usuario, false, AuthenticationManager, Request.Browser.Type);
                        var userext = _usuarioAppService.ObterUsuarioPorEmail(model.Email);
                        await _loginAppService.EfetuarLogin(userext.Result, false, AuthenticationManager, Request.Browser.Type);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            await SignOutAsync();
            //AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        private async Task SignOutAsync()
        {
            var clientKey = Request.Browser.Type;

            var user = await _usuarioAppService.ObterUsuario(User.Identity.GetUserId());

            await _loginAppService.EfetuarLogoff(user, clientKey, AuthenticationManager);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignOutEverywhere()
        {
            _loginAppService.GerarSecurityStamp(User.Identity.GetUserId());
            await SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOutClient(int clientId)
        {
            var usuario = _usuarioAppService.ObterUsuario(User.Identity.GetUserId());
            var client = usuario.Result.Clients.SingleOrDefault(c => c.Id == clientId);
            if (client != null)
                usuario.Result.Clients.Remove(client);
            _usuarioAppService.Atualizar(usuario.Result);
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            private string LoginProvider { get; }
            private string RedirectUri { get; }
            private string UserId { get; }

            public ChallengeResult(string provider, string redirectUri, string userId = null)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                    properties.Dictionary[XsrfKey] = UserId;
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}
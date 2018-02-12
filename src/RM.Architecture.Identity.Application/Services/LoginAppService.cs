using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Configuration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Application.Services
{
    public class LoginAppService : ILoginAppService
    {
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IClaimsAppService _claimsAppService;
        
        public LoginAppService(ApplicationSignInManager signInManager, ApplicationUserManager userManager, IClaimsAppService claimsAppService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _claimsAppService = claimsAppService;
        }

        public async Task<SignInStatus> ObterStatusLogin(string email, string senha, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, senha, rememberMe, true);
        }

        public async Task<SignInStatus> ObterStatusLoginTwoFactor(string provedor, string codigo, bool rememberMe)
        {
            return await _signInManager.TwoFactorSignInAsync(provedor, codigo, false, rememberMe);
        }

        public async Task EfetuarLogin(ApplicationUser usuario, bool rememberMe, IAuthenticationManager authenticationManager, string clientKey)
        {
            await _userManager.SignInClientAsync(usuario, clientKey);

            await ResetarContadorTentativasLogin(usuario);

            var claimsExternos = await _claimsAppService.ObterClaimsExternos(authenticationManager);

            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie, DefaultAuthenticationTypes.ApplicationCookie);

            authenticationManager.SignIn
            (
                new AuthenticationProperties { IsPersistent = rememberMe },
                await usuario.GenerateUserIdentityAsync(_userManager, claimsExternos)
            );
        }

        public async Task EfetuarLogoff(ApplicationUser usuario, string clientKey, IAuthenticationManager authenticationManager)
        {
            await _userManager.SignOutClientAsync(usuario, clientKey);

            authenticationManager.SignOut();
        }

        public async Task<IdentityResult> ResetarSenha(string codUsuario, string codigoSeguranca, string novaSenha)
        {
            return await _userManager.ResetPasswordAsync(codUsuario, codigoSeguranca, novaSenha);
        }

        private async Task ResetarContadorTentativasLogin(ApplicationUser usuario)
        {
            await _userManager.ResetAccessFailedCountAsync(usuario.CurrentClientId);
        }
    }
}
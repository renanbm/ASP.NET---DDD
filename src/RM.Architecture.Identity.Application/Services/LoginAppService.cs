using System.Collections.Generic;
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
        private readonly IAuthorizationAppService _authorizationAppService;
        
        public LoginAppService(ApplicationSignInManager signInManager, ApplicationUserManager userManager, IAuthorizationAppService authorizationAppService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authorizationAppService = authorizationAppService;
        }

        public async Task<SignInStatus> ObterStatusLogin(string email, string senha, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(email, senha, rememberMe, true);
        }

        public async Task<SignInStatus> ObterStatusLoginTwoFactor(string provedor, string codigo, bool rememberMe)
        {
            return await _signInManager.TwoFactorSignInAsync(provedor, codigo, false, rememberMe);
        }

        public async Task<IList<UserLoginInfo>> ConsultarLoginsUsuario(string codUsuario)
        {
            return await _userManager.GetLoginsAsync(codUsuario);
        }

        public async Task EfetuarLogin(ApplicationUser usuario, bool rememberMe, IAuthenticationManager authenticationManager, string clientKey)
        {
            await _userManager.SignInClientAsync(usuario, clientKey);

            await ResetarContadorTentativasLogin(usuario);

            var claimsExternos = await _authorizationAppService.ObterClaimsExternos(authenticationManager);

            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie, DefaultAuthenticationTypes.ApplicationCookie);

            authenticationManager.SignIn
            (
                new AuthenticationProperties { IsPersistent = rememberMe },
                await usuario.GenerateUserIdentityAsync(_userManager, claimsExternos)
            );
        }

        public async Task<IdentityResult> EfetuarLogin(ApplicationUser usuario, string clientKey)
        {
            return await _userManager.SignInClientAsync(usuario, clientKey);
        }

        public async Task EfetuarLogoff(ApplicationUser usuario, string clientKey, IAuthenticationManager authenticationManager)
        {
            await _userManager.SignOutClientAsync(usuario, clientKey);

            authenticationManager.SignOut();
        }

        public async Task<IdentityResult> IncluirSenha(string codUsuario, string senha)
        {
            return await _userManager.AddPasswordAsync(codUsuario, senha);
        }

        public async Task<IdentityResult> ResetarSenha(string codUsuario, string codigoSeguranca, string novaSenha)
        {
            return await _userManager.ResetPasswordAsync(codUsuario, codigoSeguranca, novaSenha);
        }

        public async Task<IdentityResult> AlterarSenha(string usuario, string senhaAntiga, string senhaNova)
        {
            return await _userManager.ChangePasswordAsync(usuario, senhaAntiga, senhaNova);
        }

        public async Task<IdentityResult> HabilitarTwoFactorAuthentication(string codUsuario, bool habilitado)
        {
            return await _userManager.SetTwoFactorEnabledAsync(codUsuario, habilitado);
        }

        public async Task<bool> TwoFactorAuthentication(string codUsuario)
        {
            return await _userManager.GetTwoFactorEnabledAsync(codUsuario);
        }

        public async Task<IdentityResult> AdicionarLogin(string codUsuario, UserLoginInfo login)
        {
            return await _userManager.AddLoginAsync(codUsuario, login);
        }
        public async Task<IdentityResult> RemoverLogin(string codUsuario, UserLoginInfo login)
        {
            return await _userManager.RemoveLoginAsync(codUsuario, login);
        }

        public Task<string> GerarTokenCelular(string codUsuario, string numeroCelular)
        {
            return _userManager.GenerateChangePhoneNumberTokenAsync(codUsuario, numeroCelular);
        }

        private async Task ResetarContadorTentativasLogin(ApplicationUser usuario)
        {
            await _userManager.ResetAccessFailedCountAsync(usuario.CurrentClientId);
        }
    }
}
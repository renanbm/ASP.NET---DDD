using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Application.Interfaces
{
    public interface ILoginAppService
    {
        Task<SignInStatus> ObterStatusLogin(string email, string senha, bool rememberMe);

        Task<SignInStatus> ObterStatusLoginTwoFactor(string provedor, string codigo, bool rememberMe);

        Task<bool> TwoFactorAuthentication(string codUsuario);

        Task<IList<UserLoginInfo>> ConsultarLoginsUsuario(string codUsuario);

        Task EfetuarLogin(ApplicationUser usuario, bool rememberMe, IAuthenticationManager authenticationManager, string clientKey);

        Task EfetuarLogoff(ApplicationUser usuario, string clientKey, IAuthenticationManager authenticationManager);

        Task<IdentityResult> EfetuarLogin(ApplicationUser usuario, string clientKey);

        Task<IdentityResult> IncluirSenha(string codUsuario, string senha);

        Task<IdentityResult> ResetarSenha(string codUsuario, string codigoSeguranca, string novaSenha);

        Task<IdentityResult> AlterarSenha(string usuario, string senhaAntiga, string senhaNova);

        Task<IdentityResult> HabilitarTwoFactorAuthentication(string codUsuario, bool habilitado);

        Task<IdentityResult> AdicionarLogin(string codUsuario, UserLoginInfo login);

        Task<IdentityResult> RemoverLogin(string codUsuario, UserLoginInfo login);
    }
}
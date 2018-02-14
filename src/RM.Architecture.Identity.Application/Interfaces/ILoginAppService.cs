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
        Task<IList<UserLoginInfo>> ConsultarLoginsUsuario(string codUsuario);

        Task<IList<string>> ConsultarProvedores(string codUsuario);

        Task<string> ObterUsuarioVerificado();

        Task<string> ObterTokenEmail(string codUsuario);
        
        Task<SignInStatus> ObterStatusLogin(string email, string senha, bool rememberMe);

        Task<SignInStatus> ObterStatusLoginTwoFactorAuthentication(string provedor, string codigo, bool rememberMe);

        Task<IdentityResult> IncluirLogin(string codUsuario, UserLoginInfo login);

        Task<IdentityResult> IncluirSenha(string codUsuario, string senha);

        Task<IdentityResult> AlterarSenha(string usuario, string senhaAntiga, string senhaNova);

        Task<IdentityResult> ResetarSenha(string codUsuario, string codigoSeguranca, string novaSenha);

        Task<IdentityResult> RemoverLogin(string codUsuario, UserLoginInfo login);

        Task EfetuarLogin(ApplicationUser usuario, bool rememberMe, IAuthenticationManager authenticationManager, string clientKey);

        Task<SignInStatus> EfetuarLoginExterno(ExternalLoginInfo login, bool rememberMe);

        Task EfetuarLogoff(ApplicationUser usuario, string clientKey, IAuthenticationManager authenticationManager);

        IdentityResult GerarSecurityStamp(string codUsuario);

        Task<string> GerarToken(string codUsuario);

        Task<string> GerarTokenCelular(string codUsuario, string numeroCelular);

        Task<bool> EnviarToken(string provedor);

        Task<bool> UsuarioVerificado();

        Task<IdentityResult> HabilitarTwoFactorAuthentication(string codUsuario, bool habilitado);

        Task<bool> TwoFactorAuthentication(string codUsuario);
    }
}
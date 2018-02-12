using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        #region [Funcionalidades => Usuário]

        Task<ApplicationUser> ObterUsuario(string username);
        
        Task<ApplicationUser> ObterUsuario(string username, string senha);
        
        Task<ApplicationUser> ObterUsuario(UserLoginInfo loginInfo);
        
        #endregion

        #region [Funcionalidades => E-mail]

        Task<bool> EmailConfirmado(Guid codUsuario);
        
        Task<IdentityResult> ConfirmarEmail(Guid codUsuario, string codigoVerificacao);
        
        #endregion

        #region [Funcionalidades => Claims]

        Task<ClaimsIdentity> ObterClaimsExternos(IAuthenticationManager authenticationManager);
        
        #endregion
    }
}

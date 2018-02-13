using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        IEnumerable<ApplicationUser> Listar();

        Task<ApplicationUser> ObterUsuario(string username);
        
        Task<ApplicationUser> ObterUsuario(string username, string senha);
        
        Task<ApplicationUser> ObterUsuario(UserLoginInfo loginInfo);

        Task<bool> EmailConfirmado(Guid codUsuario);
        
        Task<IdentityResult> ConfirmarEmail(Guid codUsuario, string codigoVerificacao);
    }
}

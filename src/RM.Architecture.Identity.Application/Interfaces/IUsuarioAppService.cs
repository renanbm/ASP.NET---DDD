using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using RM.Architecture.Identity.Domain.Entities;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Application.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<List<ApplicationUser>> Listar();

        IEnumerable<Usuario> ListarUsuarios();

        Usuario ObterUsuarioRepo(string codUsuario);

        Task<string> ObterTelefoneUsuario(string codUsuario);

        Task<ApplicationUser> ObterUsuarioPorEmail(string email);

        void DesativarLock(string id);

        Task<ApplicationUser> ObterUsuario(string username);

        Task<ApplicationUser> ObterUsuario(string username, string senha);

        Task<ApplicationUser> ObterUsuario(UserLoginInfo loginInfo);

        Task<bool> EmailConfirmado(Guid codUsuario);

        Task<IdentityResult> ConfirmarEmail(Guid codUsuario, string codigoVerificacao);

        Task<IdentityResult> RemoverUsuario(ApplicationUser usuario);

        Task<IdentityResult> IncluirUsuarioSenha(ApplicationUser usuario, string senha);

        Task<IdentityResult> IncluirUsuario(ApplicationUser usuario);

        Task<IdentityResult> AtualizarTelefone(string codUsuario, string telefone, string token);

        Task<IdentityResult> AtualizarTelefone(string codUsuario, string telefone);

        bool SmsService();

        Task EnviarSms(IdentityMessage mensagem);

        Task EnviarEmail(string codUsuario, string assunto, string mensagem);
    }
}

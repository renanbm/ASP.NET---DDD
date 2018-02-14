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
        
        Task<ApplicationUser> ObterUsuario(string username);

        Task<ApplicationUser> ObterUsuario(string username, string senha);

        Task<ApplicationUser> ObterUsuario(UserLoginInfo loginInfo);

        Task<ApplicationUser> ObterUsuarioPorEmail(string email);

        Usuario ObterUsuarioRepo(string codUsuario);

        Task<string> ObterTelefoneUsuario(string codUsuario);

        Task<IdentityResult> IncluirUsuarioSenha(ApplicationUser usuario, string senha);

        Task<IdentityResult> IncluirUsuario(ApplicationUser usuario);

        Task<IdentityResult> AtualizarTelefone(string codUsuario, string telefone, string token);

        Task<IdentityResult> AtualizarTelefone(string codUsuario, string telefone);

        IdentityResult Atualizar(ApplicationUser usuario);

        void DesativarLock(string id);

        Task<IdentityResult> ConfirmarEmail(Guid codUsuario, string codigoVerificacao);

        Task<IdentityResult> RemoverUsuario(ApplicationUser usuario);

        Task<bool> EmailConfirmado(Guid codUsuario);

        bool SmsService();

        Task EnviarSms(IdentityMessage mensagem);

        Task EnviarEmail(string codUsuario, string assunto, string mensagem);
    }
}

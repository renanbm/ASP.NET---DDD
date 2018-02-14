using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Domain.Entities;
using RM.Architecture.Identity.Domain.Interfaces.Repository;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Configuration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioAppService(ApplicationUserManager userManager, IUsuarioRepository usuarioRepository)
        {
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
        }

        public Task<List<ApplicationUser>> Listar()
        {
            return _userManager.Users.ToListAsync();
        }

        public IEnumerable<Usuario> ListarUsuarios()
        {
            return _usuarioRepository.ListarUsuarios();
        }

        public async Task<ApplicationUser> ObterUsuario(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<ApplicationUser> ObterUsuario(string username, string senha)
        {
            return await _userManager.FindAsync(username, senha);
        }

        public async Task<ApplicationUser> ObterUsuario(UserLoginInfo loginInfo)
        {
            return await _userManager.FindAsync(loginInfo);
        }

        public async Task<ApplicationUser> ObterUsuarioPorEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public Usuario ObterUsuarioRepo(string codUsuario)
        {
            return _usuarioRepository.ObterUsuario(codUsuario);
        }

        public async Task<string> ObterTelefoneUsuario(string codUsuario)
        {
            return await _userManager.GetPhoneNumberAsync(codUsuario);
        }

        public async Task<IdentityResult> IncluirUsuario(ApplicationUser usuario)
        {
            return await _userManager.CreateAsync(usuario);
        }

        public async Task<IdentityResult> IncluirUsuarioSenha(ApplicationUser usuario, string senha)
        {
            return await _userManager.CreateAsync(usuario, senha);
        }

        public IdentityResult Atualizar(ApplicationUser usuario)
        {
            return _userManager.Update(usuario);
        }

        public async Task<IdentityResult> AtualizarTelefone(string codUsuario, string telefone)
        {
            return await _userManager.SetPhoneNumberAsync(codUsuario, telefone);
        }

        public async Task<IdentityResult> AtualizarTelefone(string codUsuario, string telefone, string token)
        {
            return await _userManager.ChangePhoneNumberAsync(codUsuario, telefone, token);
        }

        public async Task<IdentityResult> RemoverUsuario(ApplicationUser usuario)
        {
            return await _userManager.DeleteAsync(usuario);
        }

        public void DesativarLock(string id)
        {
            _usuarioRepository.DesativarLock(id);
        }

        public async Task<IdentityResult> ConfirmarEmail(Guid codUsuario, string codigoVerificacao)
        {
            return await _userManager.ConfirmEmailAsync(codUsuario.ToString(), codigoVerificacao);
        }

        public async Task<bool> EmailConfirmado(Guid codUsuario)
        {
            return await _userManager.IsEmailConfirmedAsync(codUsuario.ToString());
        }

        public bool SmsService()
        {
            return _userManager.SmsService == null;
        }

        public Task EnviarSms(IdentityMessage mensagem)
        {
            return _userManager.SmsService.SendAsync(mensagem);
        }

        public Task EnviarEmail(string codUsuario, string assunto, string mensagem)
        {
            return _userManager.SendEmailAsync(codUsuario, assunto, mensagem);
        }
    }
}
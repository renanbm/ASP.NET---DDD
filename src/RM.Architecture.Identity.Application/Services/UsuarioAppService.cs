using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
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
        
        public Usuario ObterUsuarioRepo(string codUsuario)
        {
            return _usuarioRepository.ObterUsuario(codUsuario);
        }

        public void DesativarLock(string id)
        {
            _usuarioRepository.DesativarLock(id);
        }

        public async Task<ApplicationUser> ObterUsuario(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<ApplicationUser> ObterUsuarioPorEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> ObterUsuario(string username, string senha)
        {
            return await _userManager.FindAsync(username, senha);
        }

        public async Task<ApplicationUser> ObterUsuario(UserLoginInfo loginInfo)
        {
            return await _userManager.FindAsync(loginInfo);
        }

        public async Task<bool> EmailConfirmado(Guid codUsuario)
        {
            return await _userManager.IsEmailConfirmedAsync(codUsuario.ToString());
        }

        public async Task<IdentityResult> ConfirmarEmail(Guid codUsuario, string codigoVerificacao)
        {
            return await _userManager.ConfirmEmailAsync(codUsuario.ToString(), codigoVerificacao);
        }

        public async Task<IdentityResult> RemoverUsuario(ApplicationUser usuario)
        {
            return await _userManager.DeleteAsync(usuario);
        }

        public async Task<IdentityResult> IncluirUsuarioSenha(ApplicationUser usuario, string senha)
        {
            return await _userManager.CreateAsync(usuario, senha);
        }
        public async Task<IdentityResult> IncluirUsuario(ApplicationUser usuario)
        {
            return await _userManager.CreateAsync(usuario);
        }
    }
}
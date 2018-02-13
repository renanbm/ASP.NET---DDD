using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Configuration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly ApplicationUserManager _userManager;

        public UsuarioAppService(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<ApplicationUser> Listar()
        {
            return _userManager.Users.ToList();
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

        public async Task<bool> EmailConfirmado(Guid codUsuario)
        {
            return await _userManager.IsEmailConfirmedAsync(codUsuario.ToString());
        }

        public async Task<IdentityResult> ConfirmarEmail(Guid codUsuario, string codigoVerificacao)
        {
            return await _userManager.ConfirmEmailAsync(codUsuario.ToString(), codigoVerificacao);
        }
    }
}
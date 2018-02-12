using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Domain.Interfaces.Repository;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Configuration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        #region [Variáveis Locais]

        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IUsuarioRepository _usuarioRepository;

        #endregion

        #region [Construtor]

        public UsuarioAppService(ApplicationSignInManager signInManager, ApplicationUserManager userManager, IUsuarioRepository usuarioRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
        }

        #endregion

        #region [Funcionalidades => Usuário]

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

        #endregion

        #region [Funcionalidades => E-mail]

        public async Task<bool> EmailConfirmado(Guid codUsuario)
        {
            return await _userManager.IsEmailConfirmedAsync(codUsuario.ToString());
        }

        public async Task<IdentityResult> ConfirmarEmail(Guid codUsuario, string codigoVerificacao)
        {
            return await _userManager.ConfirmEmailAsync(codUsuario.ToString(), codigoVerificacao);
        }

        #endregion

        #region [Funcionalidades => Claims]

        private static async Task<ClaimsIdentity> ObterClaimsExternos(IAuthenticationManager authenticationManager)
        {
            var ext = await authenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
            return ext;
        }

        #endregion
    }
}
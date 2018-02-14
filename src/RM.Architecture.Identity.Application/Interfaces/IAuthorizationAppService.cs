using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using RM.Architecture.Identity.Application.ViewModels;
using RM.Architecture.Identity.Domain.Entities;

namespace RM.Architecture.Identity.Application.Interfaces
{
    public interface IAuthorizationAppService
    {
        Task<List<IdentityRole>> ListarRoles();

        List<Claims> ListarClaims();

        Task<IdentityRole> ObterRole(string codRole);

        Task<IList<string>> ObterRolesUsuario(string codUsuario);

        Task<IList<Claim>> ObterClaimsUsuario(string codUsuario);

        Task<ClaimsIdentity> ObterClaimsExternos(IAuthenticationManager authenticationManager);

        Task<IdentityResult> IncluirRole(string nome);

        Task<IdentityResult> IncluirRoleUsuario(string codUsuario, string[] roles);

        void IncluirClaim(ClaimViewModel claim);

        void IncluirClaimUsuario(string codUsuario, ClaimViewModel claim);

        Task<IdentityResult> AtualizarRole(RoleViewModel roleviewModel);

        Task<IdentityResult> Remover(IdentityRole role);

        Task<IdentityResult> RemoverClaimsUsuario(string codUsuario, string[] roles);

        Task<bool> UsuarioPossuiRole(string codUsuario, string role);
    }
}
using System.Collections.Generic;
using System.Linq;
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
        List<Claims> ListarClaims();

        IQueryable<IdentityRole> ListarRoles();

        Task<IdentityRole> ObterRole(string codRole);

        Task<ClaimsIdentity> ObterClaimsExternos(IAuthenticationManager authenticationManager);

        void IncluirClaimUsuario(string codUsuario, ClaimViewModel claim);

        Task<bool> UsuarioPossuiRole(string codUsuario, string role);

        Task<IdentityResult> IncluirRole(string nome);

        void IncluirClaim(ClaimViewModel claim);

        Task<IdentityResult> AtualizarRole(RoleViewModel roleviewModel);

        Task<IdentityResult> Remover(IdentityRole role);
    }
}
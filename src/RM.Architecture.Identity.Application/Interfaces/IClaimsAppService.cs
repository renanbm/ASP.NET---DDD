using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using RM.Architecture.Identity.Application.ViewModels;
using RM.Architecture.Identity.Domain.Entities;

namespace RM.Architecture.Identity.Application.Interfaces
{
    public interface IClaimsAppService
    {
        List<Claims> ListarClaims();

        Task<ClaimsIdentity> ObterClaimsExternos(IAuthenticationManager authenticationManager);

        void IncluirClaimUsuario(string codUsuario, ClaimViewModel claim);

        void IncluirClaim(ClaimViewModel claim);
    }
}
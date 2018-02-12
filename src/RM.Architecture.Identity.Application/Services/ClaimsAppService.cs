using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Application.ViewModels;
using RM.Architecture.Identity.Domain.Entities;
using RM.Architecture.Identity.Domain.Interfaces.Repository;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Configuration;

namespace RM.Architecture.Identity.Application.Services
{
    public class ClaimsAppService : IClaimsAppService
    {
        private readonly IClaimsRepository _claimsRepository;
        private readonly ApplicationUserManager _userManager;

        public ClaimsAppService(IClaimsRepository claimsRepository, ApplicationUserManager userManager)
        {
            _claimsRepository = claimsRepository;
            _userManager = userManager;
        }

        public List<Claims> ListarClaims()
        {
            return _claimsRepository.Listar();
        }

        public async Task<ClaimsIdentity> ObterClaimsExternos(IAuthenticationManager authenticationManager)
        {
            return await authenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
        }

        public void IncluirClaim(ClaimViewModel claim)
        {
            _claimsRepository.Incluir(new Claims
            {
                Nome = claim.Type
            });
        }

        public async void IncluirClaimUsuario(string codUsuario, ClaimViewModel claim)
        {
            await _userManager.AddClaimAsync(codUsuario, new Claim(claim.Type, claim.Value));
        }
    }
}
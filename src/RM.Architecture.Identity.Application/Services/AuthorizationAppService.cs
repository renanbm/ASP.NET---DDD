﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Application.ViewModels;
using RM.Architecture.Identity.Domain.Entities;
using RM.Architecture.Identity.Domain.Interfaces.Repository;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Configuration;

namespace RM.Architecture.Identity.Application.Services
{
    public class AuthorizationAppService : IAuthorizationAppService
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        private readonly IClaimsRepository _claimsRepository;

        public AuthorizationAppService(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IClaimsRepository claimsRepository)
        {
            _claimsRepository = claimsRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<IdentityRole>> ListarRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public List<Claims> ListarClaims()
        {
            return _claimsRepository.Listar();
        }

        public async Task<IdentityRole> ObterRole(string codRole)
        {
            return await _roleManager.FindByIdAsync(codRole);
        }

        public Task<IList<string>> ObterRolesUsuario(string codUsuario)
        {
            return _userManager.GetRolesAsync(codUsuario);
        }

        public Task<IList<Claim>> ObterClaimsUsuario(string codUsuario)
        {
            return _userManager.GetClaimsAsync(codUsuario);
        }

        public async Task<ClaimsIdentity> ObterClaimsExternos(IAuthenticationManager authenticationManager)
        {
            return await authenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
        }

        public async Task<IdentityResult> IncluirRole(string nome)
        {
            var role = new IdentityRole(nome);
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> IncluirRoleUsuario(string codUsuario, string[] roles)
        {
            return await _userManager.AddToRolesAsync(codUsuario, roles);
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

        public async Task<IdentityResult> Remover(IdentityRole role)
        {
            return await _roleManager.DeleteAsync(role);
        }

        public async Task<IdentityResult> RemoverClaimsUsuario(string codUsuario, string[] roles)
        {
            return await _userManager.RemoveFromRolesAsync(codUsuario, roles);
        }

        public async Task<IdentityResult> AtualizarRole(RoleViewModel roleviewModel)
        {
            var role = new IdentityRole
            {
                Name = roleviewModel.Name,
                Id = roleviewModel.Id
            };

            return await _roleManager.UpdateAsync(role);
        }

        public async Task<bool> UsuarioPossuiRole(string codUsuario, string role)
        {
            return await _userManager.IsInRoleAsync(codUsuario, role);
        }
    }
}
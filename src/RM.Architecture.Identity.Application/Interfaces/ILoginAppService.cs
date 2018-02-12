﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Application.Interfaces
{
    public interface ILoginAppService
    {
        Task<SignInStatus> ObterStatusLogin(string email, string senha, bool rememberMe);

        Task<SignInStatus> ObterStatusLoginTwoFactor(string provedor, string codigo, bool rememberMe);

        Task EfetuarLogin(ApplicationUser usuario, bool rememberMe, IAuthenticationManager authenticationManager, string clientKey);

        Task EfetuarLogoff(ApplicationUser usuario, string clientKey, IAuthenticationManager authenticationManager);

        Task<IdentityResult> ResetarSenha(string codUsuario, string codigoSeguranca, string novaSenha);
    }
}
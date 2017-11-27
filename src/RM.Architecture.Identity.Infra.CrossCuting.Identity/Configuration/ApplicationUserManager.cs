﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Context;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.Configuration
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

            ValidarUsuario(manager);

            ValidarSenha(manager);

            ConfigurarLockOut(manager);

            // Registrando os providers para Two Factor.
            manager.RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });

            manager.RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Código de Segurança",
                BodyFormat = "Seu código de segurança é: {0}"
            });

            // Definindo a classe de serviço de e-mail
            manager.EmailService = new EmailService();

            // Definindo a classe de serviço de SMS
            manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));

            return manager;
        }

        private static void ConfigurarLockOut(ApplicationUserManager manager)
        {
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
        }

        private static void ValidarSenha(ApplicationUserManager manager)
        {
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };
        }

        private static void ValidarUsuario(ApplicationUserManager manager)
        {
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
        }

        // Metodo para login async que guarda os dados Client conectado
        public async Task<IdentityResult> SignInClientAsync(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
                throw new ArgumentNullException(nameof(clientKey));

            var client = user.Clients.SingleOrDefault(c => c.ClientKey == clientKey);
            if (client == null)
            {
                client = new Client {ClientKey = clientKey};
                user.Clients.Add(client);
            }

            var result = await UpdateAsync(user);
            user.CurrentClientId = client.Id.ToString();
            return result;
        }

        // Metodo para login async que remove os dados Client conectado
        public async Task<IdentityResult> SignOutClientAsync(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
                throw new ArgumentNullException(nameof(clientKey));

            var client = user.Clients.SingleOrDefault(c => c.ClientKey == clientKey);
            if (client != null)
                user.Clients.Remove(client);

            user.CurrentClientId = null;
            return await UpdateAsync(user);
        }
    }
}
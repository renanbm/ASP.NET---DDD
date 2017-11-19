using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.Model
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Clients = new Collection<Client>();
        }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public override string Email { get; set; }
        
        public virtual ICollection<Client> Clients { get; set; }

        [NotMapped]
        public string CurrentClientId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager,
            ClaimsIdentity ext = null)
        {
            // Observe que o authenticationType precisa ser o mesmo que foi definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(CurrentClientId))
                claims.Add(new Claim("AspNet.Identity.ClientId", CurrentClientId));

            //  Adicione novos Claims aqui //

            // Adicionando Claims externos capturados no login
            if (ext != null)
                SetExternalProperties(userIdentity, ext);

            // Gerenciamento de Claims para informaçoes do usuario
            //claims.Add(new Claim("AdmRoles", "True"));

            userIdentity.AddClaims(claims);

            return userIdentity;
        }

        private static void SetExternalProperties(ClaimsIdentity identity, ClaimsIdentity ext)
        {
            if (ext == null) return;

            const string ignoreClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";

            // Adicionando Claims Externos no Identity
            foreach (var c in ext.Claims)
                if (!c.Type.StartsWith(ignoreClaim))
                    if (!identity.HasClaim(c.Type, c.Value))
                        identity.AddClaim(c);
        }
    }
}
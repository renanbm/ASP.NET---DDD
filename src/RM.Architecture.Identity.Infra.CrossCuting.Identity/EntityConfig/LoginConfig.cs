using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class LoginConfig : EntityTypeConfiguration<IdentityUserLogin>
    {
        public LoginConfig()
        {
            HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

            ToTable("Logins");
        }
    }
}
using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class LoginConfig : EntityTypeConfiguration<IdentityUserLogin>
    {
        public LoginConfig()
        {
            ToTable("Logins");
        }
    }
}
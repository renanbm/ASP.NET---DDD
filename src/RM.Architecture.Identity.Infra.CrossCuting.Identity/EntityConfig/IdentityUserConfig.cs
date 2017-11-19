using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class IdentityUserConfig : EntityTypeConfiguration<IdentityUser>
    {
        public IdentityUserConfig()
        {
            Property(u => u.Id)
                .HasColumnName("CodUsuario");

            ToTable("Usuarios");
        }      
    }
}
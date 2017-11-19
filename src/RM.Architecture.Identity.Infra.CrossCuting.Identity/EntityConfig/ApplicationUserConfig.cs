using System.Data.Entity.ModelConfiguration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class ApplicationUserConfig : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfig()
        {
            Property(p => p.Id)
                .HasColumnName("CodUsuario");

            ToTable("Usuarios");
        }
    }
}
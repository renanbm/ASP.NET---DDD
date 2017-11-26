using System.Data.Entity.ModelConfiguration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class ApplicationUserConfig : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfig()
        {
            HasKey(u => u.Id);

            Property(p => p.Id)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("CodUsuario");

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256);

            Ignore(p => p.CurrentClientId);

            ToTable("Usuarios");
        }
    }
}
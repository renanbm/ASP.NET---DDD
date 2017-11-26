using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class IdentityUserConfig : EntityTypeConfiguration<IdentityUser>
    {
        public IdentityUserConfig()
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

            ToTable("Usuarios");
        }
    }
}
using System.Data.Entity.ModelConfiguration;
using RM.Architecture.Identity.Domain.Entities;

namespace RM.Architecture.Identity.Infra.Data.EntityConfig
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            HasKey(u => u.Id);

            Property(u => u.Id)
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
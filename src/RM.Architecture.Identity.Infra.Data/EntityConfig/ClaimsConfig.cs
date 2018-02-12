using System.Data.Entity.ModelConfiguration;
using RM.Architecture.Identity.Domain.Entities;

namespace RM.Architecture.Identity.Infra.Data.EntityConfig
{
    public class ClaimsConfig : EntityTypeConfiguration<Claims>
    {
        public ClaimsConfig()
        {
            HasKey(u => u.CodClaim);

            Property(u => u.CodClaim)
                .IsRequired()
                .HasColumnName("CodClaim");

            Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(128);

            ToTable("Claims");
        }
    }
}
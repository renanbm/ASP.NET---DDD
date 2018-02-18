using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class ClaimsConfig : EntityTypeConfiguration<Claims>
    {
        public ClaimsConfig()
        {
            HasKey(u => u.CodClaim);

            Property(u => u.CodClaim)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("CodClaim");

            Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(128);

            ToTable("Claims");
        }
    }
}
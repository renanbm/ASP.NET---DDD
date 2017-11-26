using System.Data.Entity.ModelConfiguration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class ClaimsConfig : EntityTypeConfiguration<Claims>
    {
        public ClaimsConfig()
        {
            ToTable("Claims");
        }
    }
}
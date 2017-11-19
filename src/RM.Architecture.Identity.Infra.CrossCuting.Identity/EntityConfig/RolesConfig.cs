using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class RolesConfig : EntityTypeConfiguration<IdentityRole>
    {
        public RolesConfig()
        {
            ToTable("Roles");
        }      
    }
}
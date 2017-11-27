using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class UsuarioRolesConfig : EntityTypeConfiguration<IdentityUserRole>
    {
        public UsuarioRolesConfig()
        {
            HasKey(c => new {c.UserId, c.RoleId});

            ToTable("UsuarioRoles");
        }
    }
}
using System.Data.Entity;
using RM.Architecture.Identity.Domain.Entities;
using RM.Architecture.Identity.Infra.Data.EntityConfig;

namespace RM.Architecture.Identity.Infra.Data.Context
{
    public class IdentityContext : DbContext
    {
        public IdentityContext() : base("DefaultConnection")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
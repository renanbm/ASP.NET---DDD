using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", false)
        {
        }

        public DbSet<Client> Client { get; set; }

        public DbSet<Claims> Claims { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IdentityUserConfig());
            modelBuilder.Configurations.Add(new ApplicationUserConfig());
            modelBuilder.Configurations.Add(new LoginConfig());
            modelBuilder.Configurations.Add(new RolesConfig());
            modelBuilder.Configurations.Add(new UsuarioRolesConfig());
            modelBuilder.Configurations.Add(new UsuarioClaimsConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
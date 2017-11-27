using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            
            modelBuilder.Properties()
                .Where(p => p.Name == "Cod" + p.ReflectedType?.Name)
                .Configure(p => p.IsKey());

            modelBuilder.Properties<Guid>()
                .Configure(p => p.HasColumnType("uniqueidentifier"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));
            
            modelBuilder.Configurations.Add(new IdentityUserConfig());
            modelBuilder.Configurations.Add(new ApplicationUserConfig());
            modelBuilder.Configurations.Add(new LoginConfig());
            modelBuilder.Configurations.Add(new RolesConfig());
            modelBuilder.Configurations.Add(new ClaimsConfig());
            modelBuilder.Configurations.Add(new UsuarioRolesConfig());
            modelBuilder.Configurations.Add(new UsuarioClaimsConfig());
            modelBuilder.Configurations.Add(new ClientsConfig());
        }
    }
}
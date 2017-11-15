using System.Data.Entity.Migrations;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Context;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
        }
    }
}
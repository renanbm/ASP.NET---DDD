using System.Data.Entity.Migrations;
using RM.Architecture.Identity.Infra.Data.Context;

namespace RM.Architecture.Identity.Infra.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<IdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RM.Architecture.Identity.Infra.Data.Context.IdentityContext";
        }

        protected override void Seed(IdentityContext context)
        {
        }
    }
}
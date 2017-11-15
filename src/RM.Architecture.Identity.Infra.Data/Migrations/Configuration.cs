namespace RM.Architecture.Identity.Infra.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Context.IdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "RM.Architecture.Identity.Infra.Data.Context.IdentityContext";
        }

        protected override void Seed(Context.IdentityContext context)
        {
        }
    }
}

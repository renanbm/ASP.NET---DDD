using System.Data.Entity.Migrations;
using RM.Architecture.Filiacao.Infrastructure.Data.Context;

namespace RM.Architecture.Filiacao.Infrastructure.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ArchitectureContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ArchitectureContext context)
        {
        }
    }
}
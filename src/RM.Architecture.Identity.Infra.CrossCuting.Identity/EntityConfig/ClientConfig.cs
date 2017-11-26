using System.Data.Entity.ModelConfiguration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class ClientsConfig : EntityTypeConfiguration<Client>
    {
        public ClientsConfig()
        {
            ToTable("AspNetClients");
        }
    }
}
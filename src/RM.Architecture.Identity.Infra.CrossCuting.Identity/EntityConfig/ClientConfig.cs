using System.Data.Entity.ModelConfiguration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.EntityConfig
{
    public class ClientsConfig : EntityTypeConfiguration<Client>
    {
        public ClientsConfig()
        {
            Property(p => p.Id).HasColumnName("CodClient");
            ToTable("AspNetClients");
        }
    }
}
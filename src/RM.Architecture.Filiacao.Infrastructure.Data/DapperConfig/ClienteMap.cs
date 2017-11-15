using Dapper.FluentMap.Mapping;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;

namespace RM.Architecture.Filiacao.Infrastructure.Data.DapperConfig
{
    public class ClienteMap : EntityMap<Cliente>
    {
        public ClienteMap()
        {
            Map(c => c.Cpf.Numero).ToColumn("Cpf");
            Map(c => c.Email.Endereco).ToColumn("Email");
        }
    }
}
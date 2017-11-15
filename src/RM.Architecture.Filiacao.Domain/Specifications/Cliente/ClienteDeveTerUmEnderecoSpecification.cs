using System.Linq;
using DomainValidation.Interfaces.Specification;

namespace RM.Architecture.Filiacao.Domain.Specifications.Cliente
{
    public class ClienteDeveTerUmEnderecoSpecification : ISpecification<Entities.Cliente.Cliente>
    {
        public bool IsSatisfiedBy(Entities.Cliente.Cliente cliente)
        {
            return cliente.Enderecos != null && cliente.Enderecos.Any();
        }
    }
}
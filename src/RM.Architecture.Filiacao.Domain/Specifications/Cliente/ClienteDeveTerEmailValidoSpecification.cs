using DomainValidation.Interfaces.Specification;

namespace RM.Architecture.Filiacao.Domain.Specifications.Cliente
{
    public class ClienteDeveTerEmailValidoSpecification : ISpecification<Entities.Cliente.Cliente>
    {
        public bool IsSatisfiedBy(Entities.Cliente.Cliente cliente)
        {
            return cliente.Email.Validar();
        }
    }
}
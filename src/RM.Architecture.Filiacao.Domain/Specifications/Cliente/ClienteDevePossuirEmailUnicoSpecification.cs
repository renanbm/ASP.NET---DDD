using DomainValidation.Interfaces.Specification;
using RM.Architecture.Filiacao.Domain.Interfaces.Repository;

namespace RM.Architecture.Filiacao.Domain.Specifications.Cliente
{
    public class ClienteDevePossuirEmailUnicoSpecification : ISpecification<Entities.Cliente.Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteDevePossuirEmailUnicoSpecification(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public bool IsSatisfiedBy(Entities.Cliente.Cliente cliente)
        {
            return _clienteRepository.ObterPorEmail(cliente.Email.Endereco) == null;
        }
    }
}
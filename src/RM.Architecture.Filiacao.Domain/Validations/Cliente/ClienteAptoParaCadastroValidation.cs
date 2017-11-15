using DomainValidation.Validation;
using RM.Architecture.Filiacao.Domain.Interfaces.Repository;
using RM.Architecture.Filiacao.Domain.Specifications.Cliente;

namespace RM.Architecture.Filiacao.Domain.Validations.Cliente
{
    public class ClienteAptoParaCadastroValidation : Validator<Entities.Cliente.Cliente>
    {
        public ClienteAptoParaCadastroValidation(IClienteRepository clienteRepository)
        {
            var cpfDuplicado = new ClienteDevePossuirCpfUnicoSpecification(clienteRepository);
            var emailDuplicado = new ClienteDevePossuirEmailUnicoSpecification(clienteRepository);
            var clienteEndereco = new ClienteDeveTerUmEnderecoSpecification();

            base.Add("cpfDuplicado",
                new Rule<Entities.Cliente.Cliente>(cpfDuplicado, "CPF já cadastrado! Esqueceu sua senha?"));
            base.Add("emailDuplicado",
                new Rule<Entities.Cliente.Cliente>(emailDuplicado, "E-mail já cadastrado! Esqueceu sua senha?"));
            base.Add("clienteEndereco",
                new Rule<Entities.Cliente.Cliente>(clienteEndereco, "Cliente não informou um endereço"));
        }
    }
}
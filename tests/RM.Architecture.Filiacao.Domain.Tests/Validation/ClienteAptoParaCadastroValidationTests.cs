using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;
using RM.Architecture.Filiacao.Domain.Entities.Endereco;
using RM.Architecture.Filiacao.Domain.Interfaces.Repository;
using RM.Architecture.Filiacao.Domain.Validations.Cliente;
using RM.Architecture.Filiacao.Domain.Value_Objects;
using RM.Architecture.Filiacao.Domain.Value_Objects.Documentos;

namespace RM.Architecture.Filiacao.Domain.Tests.Validation
{
    [TestClass]
    public class ClienteAptoParaCadastroValidationTests
    {
        [TestMethod]
        public void ClienteApto_IsValid_True()
        {
            // Arrange
            var cliente = new Cliente
            {
                Cpf = new Cpf {Numero = "82789431094"},
                Email = new Email {Endereco = "teste@cliente.com"},
                Enderecos = new List<Endereco>
                {
                    new Endereco
                    {
                        Logradouro = "Alameda Barros",
                        Numero = "676",
                        Complemento = "Apartamento 101",
                        Bairro = "Santa Cecilia",
                        Cep = "01232000"
                        //Estado = "Sao Paulo",
                        //Cidade = "Sao Paulo"
                    }
                }
            };

            // Act
            var repo = MockRepository.GenerateStub<IClienteRepository>();
            repo.Stub(s => s.ObterPorCpf(cliente.Cpf.Numero)).Return(null);
            repo.Stub(s => s.ObterPorEmail(cliente.Email.Endereco)).Return(null);

            var validationReturn = new ClienteAptoParaCadastroValidation(repo).Validate(cliente);

            // Assert
            Assert.IsTrue(validationReturn.IsValid);
        }

        [TestMethod]
        public void ClienteApto_IsValid_False()
        {
            // Arrange
            var cliente = new Cliente
            {
                Cpf = new Cpf {Numero = "82789431094"},
                Email = new Email {Endereco = "teste@cliente.com"}
            };

            // Act
            var repo = MockRepository.GenerateStub<IClienteRepository>();
            repo.Stub(s => s.ObterPorCpf(cliente.Cpf.Numero)).Return(cliente);
            repo.Stub(s => s.ObterPorEmail(cliente.Email.Endereco)).Return(cliente);

            var validationReturn = new ClienteAptoParaCadastroValidation(repo).Validate(cliente);

            // Assert
            Assert.IsFalse(validationReturn.IsValid);
            Assert.IsTrue(validationReturn.Erros.Any(e => e.Message == "CPF já cadastrado! Esqueceu sua senha?"));
            Assert.IsTrue(validationReturn.Erros.Any(e => e.Message == "E-mail já cadastrado! Esqueceu sua senha?"));
            Assert.IsTrue(validationReturn.Erros.Any(e => e.Message == "Cliente não informou um endereço"));
        }
    }
}
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;
using RM.Architecture.Filiacao.Domain.Value_Objects;
using RM.Architecture.Filiacao.Domain.Value_Objects.Documentos;

namespace RM.Architecture.Filiacao.Domain.Tests.Entity
{
    [TestClass]
    public class ClienteTests
    {
        // AAA => Arrange, Act, Assert

        [TestMethod]
        public void Cliente_SelfValidation_IsValid()
        {
            // Arrange
            var cliente = new Cliente
            {
                Cpf = new Cpf {Numero = "82789431094"},
                Email = new Email {Endereco = "teste@cliente.com"},
                DataNascimento = new DateTime(1980, 01, 01)
            };

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Cliente_SelfValidation_IsNotValid()
        {
            // Arrange
            var cliente = new Cliente
            {
                Cpf = new Cpf {Numero = "82789431093"},
                Email = new Email {Endereco = "teste2cliente.com"},
                DataNascimento = new DateTime(2016, 01, 01)
            };

            // Act
            var result = cliente.EhValido();

            // Assert
            Assert.IsFalse(result);
            Assert.IsTrue(cliente.ValidationResult.Erros.Any(e => e.Message == "Cliente informou um CPF inválido."));
            Assert.IsTrue(cliente.ValidationResult.Erros.Any(e => e.Message == "Cliente informou um e-mail inválido."));
            Assert.IsTrue(
                cliente.ValidationResult.Erros.Any(e => e.Message == "Cliente não tem maioridade para cadastro."));
        }
    }
}
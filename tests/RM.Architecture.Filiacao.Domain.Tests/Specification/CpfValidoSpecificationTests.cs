using Microsoft.VisualStudio.TestTools.UnitTesting;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;
using RM.Architecture.Filiacao.Domain.Specifications.Cliente;
using RM.Architecture.Filiacao.Domain.Value_Objects.Documentos;

namespace RM.Architecture.Filiacao.Domain.Tests.Specification
{
    [TestClass]
    public class CpfValidoSpecificationTests
    {
        [TestMethod]
        public void CpfSpecification_IsSatisfied_True()
        {
            // Arrange
            var cliente = new Cliente
            {
                Cpf = new Cpf {Numero = "82789431094"}
            };

            // Act
            var specReturn = new ClienteDeveTerCpfValidoSpecification().IsSatisfiedBy(cliente);

            // Assert
            Assert.IsTrue(specReturn);
        }

        [TestMethod]
        public void CpfSpecification_IsSatisfied_False()
        {
            // Arrange
            var cliente = new Cliente
            {
                Cpf = new Cpf {Numero = "82789431093"}
            };

            // Act
            var specReturn = new ClienteDeveTerCpfValidoSpecification().IsSatisfiedBy(cliente);

            // Assert
            Assert.IsFalse(specReturn);
        }
    }
}
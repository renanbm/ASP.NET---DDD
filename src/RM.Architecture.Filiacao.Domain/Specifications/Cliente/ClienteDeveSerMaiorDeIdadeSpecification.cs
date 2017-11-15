using System;
using DomainValidation.Interfaces.Specification;

namespace RM.Architecture.Filiacao.Domain.Specifications.Cliente
{
    public class ClienteDeveSerMaiorDeIdadeSpecification : ISpecification<Entities.Cliente.Cliente>
    {
        public bool IsSatisfiedBy(Entities.Cliente.Cliente cliente)
        {
            // TODO: Corrigir a logica para funcionar após aniversário.
            return DateTime.Now.Year - cliente.DataNascimento.Year >= 18;
        }
    }
}
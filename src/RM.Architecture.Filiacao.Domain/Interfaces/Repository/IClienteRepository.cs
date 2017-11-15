using System.Collections.Generic;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;

namespace RM.Architecture.Filiacao.Domain.Interfaces.Repository
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        IEnumerable<Cliente> ConsultarAtivos();

        Cliente ObterPorCpf(string cpf);

        Cliente ObterPorEmail(string email);
    }
}
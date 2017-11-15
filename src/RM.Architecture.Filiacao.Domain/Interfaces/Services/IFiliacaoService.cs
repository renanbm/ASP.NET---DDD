using System;
using System.Collections.Generic;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;
using RM.Architecture.Filiacao.Domain.Entities.Endereco;

namespace RM.Architecture.Filiacao.Domain.Interfaces.Services
{
    public interface IFiliacaoService : IDisposable
    {
        IEnumerable<Cliente> ListarClientes();

        Cliente ObterCliente(Guid id);

        Cliente ObterClientePorCpf(string cpf);

        Cliente ObterClientePorEmail(string email);

        IEnumerable<Cliente> ConsultarClientesAtivos();

        Cliente AdicionarCliente(Cliente cliente);

        Cliente AtualizarCliente(Cliente cliente);

        void RemoverCliente(Guid id);

        Endereco ObterEndereco(Guid id);

        Endereco AdicionarEndereco(Endereco endereco);

        Endereco AtualizarEndereco(Endereco endereco);

        void RemoverEndereco(Guid id);
    }
}
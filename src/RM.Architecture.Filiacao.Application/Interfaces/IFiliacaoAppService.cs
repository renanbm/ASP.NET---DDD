using System;
using System.Collections.Generic;
using RM.Architecture.Filiacao.Application.ViewModels;

namespace RM.Architecture.Filiacao.Application.Interfaces
{
    public interface IFiliacaoAppService : IDisposable
    {
        IEnumerable<ClienteViewModel> Listar();

        ClienteViewModel Obter(Guid id);

        ClienteViewModel ObterPorCpf(string cpf);

        ClienteViewModel ObterPorEmail(string email);

        ClienteEnderecoViewModel Adicionar(ClienteEnderecoViewModel clienteEnderecoViewModel);

        ClienteViewModel Atualizar(ClienteViewModel clienteViewModel);

        void Remover(Guid id);
    }
}
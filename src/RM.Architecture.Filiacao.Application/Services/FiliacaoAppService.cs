using System;
using System.Collections.Generic;
using AutoMapper;
using RM.Architecture.Filiacao.Application.Interfaces;
using RM.Architecture.Filiacao.Application.ViewModels;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;
using RM.Architecture.Filiacao.Domain.Entities.Endereco;
using RM.Architecture.Filiacao.Domain.Interfaces.Services;
using RM.Architecture.Filiacao.Infrastructure.Data.UnitOfWork;

namespace RM.Architecture.Filiacao.Application.Services
{
    public class FiliacaoAppService : BaseAppService, IFiliacaoAppService
    {
        private readonly IFiliacaoService _filiacaoService;

        public FiliacaoAppService(IFiliacaoService filiacaoService, IUnitOfWork uow)
            : base(uow)
        {
            _filiacaoService = filiacaoService;
        }

        public IEnumerable<ClienteViewModel> Listar()
        {
            return Mapper.Map<IEnumerable<ClienteViewModel>>(_filiacaoService.ListarClientes());
        }

        public ClienteViewModel Obter(Guid id)
        {
            return Mapper.Map<ClienteViewModel>(_filiacaoService.ObterCliente(id));
        }

        public ClienteViewModel ObterPorCpf(string cpf)
        {
            return Mapper.Map<ClienteViewModel>(_filiacaoService.ObterClientePorCpf(cpf));
        }

        public ClienteViewModel ObterPorEmail(string email)
        {
            return Mapper.Map<ClienteViewModel>(_filiacaoService.ObterClientePorEmail(email));
        }

        public ClienteEnderecoViewModel Adicionar(ClienteEnderecoViewModel clienteEnderecoViewModel)
        {
            var cliente = Mapper.Map<Cliente>(clienteEnderecoViewModel.ClienteViewModel);
            var endereco = Mapper.Map<Endereco>(clienteEnderecoViewModel.EnderecoViewModel);

            cliente.Enderecos.Add(endereco);

            var clienteReturn = _filiacaoService.AdicionarCliente(cliente);

            if (clienteReturn.ValidationResult.IsValid)
                Commit();

            clienteEnderecoViewModel.ClienteViewModel = Mapper.Map<ClienteViewModel>(clienteReturn);

            return clienteEnderecoViewModel;
        }

        public ClienteViewModel Atualizar(ClienteViewModel clienteViewModel)
        {
            var cliente = Mapper.Map<Cliente>(clienteViewModel);

            var clienteReturn = _filiacaoService.AtualizarCliente(cliente);

            return Mapper.Map<ClienteViewModel>(clienteReturn);
        }

        public void Remover(Guid id)
        {
            _filiacaoService.RemoverCliente(id);
        }

        public void Dispose()
        {
            _filiacaoService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
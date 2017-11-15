using System;
using System.Collections.Generic;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;
using RM.Architecture.Filiacao.Domain.Entities.Endereco;
using RM.Architecture.Filiacao.Domain.Interfaces.Repository;
using RM.Architecture.Filiacao.Domain.Interfaces.Services;
using RM.Architecture.Filiacao.Domain.Validations.Cliente;

namespace RM.Architecture.Filiacao.Domain.Services
{
    public class FiliacaoService : IFiliacaoService
    {
        #region [Construtor]

        public FiliacaoService(IClienteRepository clienteRepository, IEnderecoRepository enderecoRepository)
        {
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
        }

        #endregion

        #region [Dispose]

        public void Dispose()
        {
            _clienteRepository.Dispose();
            _enderecoRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion

        #region [Variáveis Locais]

        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        #endregion

        #region [Cliente]

        public IEnumerable<Cliente> ListarClientes()
        {
            return _clienteRepository.Listar();
        }

        public Cliente ObterCliente(Guid id)
        {
            return _clienteRepository.Obter(id);
        }

        public Cliente ObterClientePorCpf(string cpf)
        {
            return _clienteRepository.ObterPorCpf(cpf);
        }

        public Cliente ObterClientePorEmail(string email)
        {
            return _clienteRepository.ObterPorEmail(email);
        }

        public IEnumerable<Cliente> ConsultarClientesAtivos()
        {
            return _clienteRepository.ConsultarAtivos();
        }

        public Cliente AdicionarCliente(Cliente cliente)
        {
            if (!cliente.EhValido())
                return cliente;

            cliente.ValidationResult = new ClienteAptoParaCadastroValidation(_clienteRepository).Validate(cliente);

            return !cliente.ValidationResult.IsValid ? cliente : _clienteRepository.Adicionar(cliente);
        }

        public Cliente AtualizarCliente(Cliente cliente)
        {
            return _clienteRepository.Atualizar(cliente);
        }

        public void RemoverCliente(Guid id)
        {
            _clienteRepository.Remover(id);
        }

        #endregion

        #region [Endereço]

        public Endereco ObterEndereco(Guid id)
        {
            return _enderecoRepository.Obter(id);
        }

        public Endereco AdicionarEndereco(Endereco endereco)
        {
            return _enderecoRepository.Adicionar(endereco);
        }

        public Endereco AtualizarEndereco(Endereco endereco)
        {
            return _enderecoRepository.Atualizar(endereco);
        }

        public void RemoverEndereco(Guid id)
        {
            _enderecoRepository.Remover(id);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using RM.Architecture.Filiacao.Domain.Entities.Cliente;
using RM.Architecture.Filiacao.Domain.Entities.Endereco;
using RM.Architecture.Filiacao.Domain.Interfaces.Repository;
using RM.Architecture.Filiacao.Infrastructure.Data.Context;

namespace RM.Architecture.Filiacao.Infrastructure.Data.Repository
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ArchitectureContext context)
            : base(context)
        {
        }

        public override IEnumerable<Cliente> Listar()
        {
            var cn = Db.Database.Connection;

            const string sql = "SELECT * FROM Clientes";

            var clientes = cn.Query<Cliente, string, string, Cliente>(sql,
                (c, cpf, email) =>
                {
                    c.Cpf.Numero = cpf;
                    c.Email.Endereco = email;
                    return c;
                }, splitOn: "CodCliente, cpf, email");

            return clientes;
        }

        public IEnumerable<Cliente> ConsultarAtivos()
        {
            return Consultar(c => c.Ativo);
        }

        public override Cliente Obter(Guid id)
        {
            var cn = Db.Database.Connection;

            const string sql = @"SELECT * FROM Clientes AS C " +
                               "LEFT JOIN Enderecos AS E " +
                               "ON C.CodCliente = E.CodCliente " +
                               "WHERE C.CodCliente = @sid";

            var cliente = cn.Query<Cliente, Endereco, string, string, Cliente>(sql,
                (c, e, cpf, email) =>
                {
                    c.Enderecos.Add(e);
                    c.Cpf.Numero = cpf;
                    c.Email.Endereco = email;
                    return c;
                }, new {sid = id}, splitOn: "CodCliente, CodEndereco, Cpf, Email");

            return cliente.FirstOrDefault();
        }

        public Cliente ObterPorCpf(string cpf)
        {
            return Consultar(c => c.Cpf.Numero == cpf).FirstOrDefault();
        }

        public Cliente ObterPorEmail(string email)
        {
            return Consultar(c => c.Email.Endereco == email).FirstOrDefault();
        }

        public override void Remover(Guid id)
        {
            var cliente = Obter(id);
            cliente.Ativo = false;
            Atualizar(cliente);
        }
    }
}
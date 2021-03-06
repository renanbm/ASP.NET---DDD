﻿using DomainValidation.Validation;
using RM.Architecture.Filiacao.Domain.Specifications.Cliente;

namespace RM.Architecture.Filiacao.Domain.Validations.Cliente
{
    public class ClienteEstaConsistenteValidation : Validator<Entities.Cliente.Cliente>
    {
        public ClienteEstaConsistenteValidation()
        {
            var cpfCliente = new ClienteDeveTerCpfValidoSpecification();
            var clienteEmail = new ClienteDeveTerEmailValidoSpecification();
            var clienteMaioridade = new ClienteDeveSerMaiorDeIdadeSpecification();

            Add("CpfValido", new Rule<Entities.Cliente.Cliente>(cpfCliente, "Cliente informou um CPF inválido."));
            Add("clienteEmail",
                new Rule<Entities.Cliente.Cliente>(clienteEmail, "Cliente informou um e-mail inválido."));
            Add("clienteMaioridade",
                new Rule<Entities.Cliente.Cliente>(clienteMaioridade, "Cliente não tem maioridade para cadastro."));
        }
    }
}
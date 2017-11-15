using System;
using System.Collections.Generic;
using DomainValidation.Validation;
using RM.Architecture.Filiacao.Domain.Validations.Cliente;
using RM.Architecture.Filiacao.Domain.Value_Objects;
using RM.Architecture.Filiacao.Domain.Value_Objects.Documentos;

namespace RM.Architecture.Filiacao.Domain.Entities.Cliente
{
    public class Cliente
    {
        public Cliente()
        {
            CodCliente = Guid.NewGuid();
            Enderecos = new List<Endereco.Endereco>();
            Cpf = new Cpf();
            Email = new Email();
        }

        public Guid CodCliente { get; set; }

        public string Nome { get; set; }

        public Cpf Cpf { get; set; }

        public Email Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<Endereco.Endereco> Enderecos { get; set; }

        public ValidationResult ValidationResult { get; set; }

        public bool EhValido()
        {
            ValidationResult = new ClienteEstaConsistenteValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        //protected Cliente()
        //{
        //    CodCliente = Guid.NewGuid();
        //    Enderecos = new List<Endereco.Endereco>();
        //    Email = new Email();
        //}

        //public Guid CodCliente { get; set; }

        //public int CodTipoPessoa { get; set; }

        //public Email Email { get; set; }

        //public DateTime DataCadastro { get; set; }

        //public virtual ICollection<Endereco.Endereco> Enderecos { get; set; }

        //public bool Ativo { get; set; }

        //public ValidationResult ValidationResult { get; set; }

        //public bool EhValido()
        //{
        //    ValidationResult = new ClienteEstaConsistenteValidation().Validate(this);
        //    return ValidationResult.IsValid;
        //}

        //public abstract int ObterTipoPessoa();

        //public abstract string ObterNome();

        //public abstract string ObterDocumento();
    }
}
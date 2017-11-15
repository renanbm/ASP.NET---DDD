using System;
using RM.Architecture.Filiacao.Domain.Value_Objects;
using RM.Architecture.Filiacao.Domain.Value_Objects.Documentos;

namespace RM.Architecture.Filiacao.Domain.Entities.Cliente
{
    public class ClientePessoaFisica //: Cliente
    {
        public ClientePessoaFisica()
        {
            Cpf = new Cpf();
            Rg = new Rg();
            Sexo = new Sexo();
        }

        public string Nome { get; set; }

        public Cpf Cpf { get; set; }

        public Rg Rg { get; set; }

        public DateTime DataNascimento { get; set; }

        public Sexo Sexo { get; set; }

        //public override int ObterTipoPessoa()
        //{
        //    return (int)EnumTipoPessoa.Fisica;
        //}

        //public override string ObterNome()
        //{
        //    return Nome;
        //}

        //public override string ObterDocumento()
        //{
        //    return Cpf.Numero;
        //}
    }
}
using System;
using RM.Architecture.Filiacao.Domain.Value_Objects.Endereco;

namespace RM.Architecture.Filiacao.Domain.Entities.Endereco
{
    public class Endereco
    {
        public Endereco()
        {
            CodEndereco = Guid.NewGuid();
        }

        public Guid CodEndereco { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cep { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        //public Cidade Cidade { get; set; }

        //public Estado Estado { get; set; }

        public Guid CodCliente { get; set; }

        public virtual Cliente.Cliente Cliente { get; set; }
    }
}
using System;
using RM.Architecture.Filiacao.Domain.Value_Objects;
using RM.Architecture.Filiacao.Domain.Value_Objects.Documentos;

namespace RM.Architecture.Filiacao.Domain.Entities.Cliente
{
    public class ClienteEstrangeiro //: Cliente
    {
        public ClienteEstrangeiro()
        {
            Passaporte = new Passaporte();
            Sexo = new Sexo();
        }

        public string Nome { get; set; }

        public Sexo Sexo { get; set; }

        public DateTime DataNascimento { get; set; }

        public Passaporte Passaporte { get; set; }

        //public override string ObterNome()
        //}
        //    return (int)EnumTipoPessoa.Estrangeira;
        //{

        //public override int ObterTipoPessoa()
        //{
        //    return Nome;
        //}

        //public override string ObterDocumento()
        //{
        //    return Passaporte.NumeroPassaporte;
        //}
    }
}
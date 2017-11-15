using RM.Architecture.Filiacao.Domain.Value_Objects;
using RM.Architecture.Filiacao.Domain.Value_Objects.Documentos;

namespace RM.Architecture.Filiacao.Domain.Entities.Cliente
{
    public class ClientePessoaJuridica //: Cliente
    {
        public ClientePessoaJuridica()
        {
            Cnpj = new Cnpj();
            RepresentanteEmpresa = new RepresentanteEmpresa();
        }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public Cnpj Cnpj { get; set; }

        public string InscricaoEstadual { get; set; }

        public RepresentanteEmpresa RepresentanteEmpresa { get; set; }

        //public override int ObterTipoPessoa()
        //{
        //    return (int)EnumTipoPessoa.Juridica;
        //}

        //public override string ObterNome()
        //{
        //    return NomeFantasia;
        //}

        //public override string ObterDocumento()
        //{
        //    return Cnpj.Numero;
        //}
    }
}
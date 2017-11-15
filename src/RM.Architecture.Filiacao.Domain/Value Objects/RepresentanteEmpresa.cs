using RM.Architecture.Filiacao.Domain.Value_Objects.Documentos;

namespace RM.Architecture.Filiacao.Domain.Value_Objects
{
    public class RepresentanteEmpresa
    {
        public string Nome { get; set; }

        public string Cargo { get; set; }

        public Cpf Cpf { get; set; }
    }
}
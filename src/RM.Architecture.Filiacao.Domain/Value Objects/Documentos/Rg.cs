using System;
using RM.Architecture.Filiacao.Domain.Value_Objects.Endereco;

namespace RM.Architecture.Filiacao.Domain.Value_Objects.Documentos
{
    public class Rg
    {
        public string Numero { get; set; }

        public Estado Estado { get; set; }

        public string OrgaoEmissor { get; set; }

        public DateTime DataEmissao { get; set; }
    }
}
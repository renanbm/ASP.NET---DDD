using System;
using RM.Architecture.Filiacao.Domain.Value_Objects.Endereco;

namespace RM.Architecture.Filiacao.Domain.Value_Objects.Documentos
{
    public class Passaporte
    {
        public string NumeroPassaporte { get; set; }

        public Pais Pais { get; set; }

        public DateTime DataEmissao { get; set; }

        public DateTime DataValidade { get; set; }
    }
}
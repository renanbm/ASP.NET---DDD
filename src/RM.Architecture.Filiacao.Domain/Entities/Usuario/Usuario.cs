using RM.Architecture.Filiacao.Domain.Value_Objects;
using RM.Architecture.Filiacao.Domain.Value_Objects.Documentos;

namespace RM.Architecture.Filiacao.Domain.Entities.Usuario
{
    public class Usuario
    {
        public int CodUsuario { get; set; }

        public string Login { get; set; }

        public string Nome { get; set; }

        public Email Email { get; set; }

        public Cpf Cpf { get; set; }

        public bool Ativo { get; set; }
    }
}
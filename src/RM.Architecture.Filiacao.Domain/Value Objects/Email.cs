using System.Text.RegularExpressions;

namespace RM.Architecture.Filiacao.Domain.Value_Objects
{
    public class Email
    {
        public string Endereco { get; set; }

        public bool Validar()
        {
            return Validar(Endereco);
        }

        public bool Validar(string email)
        {
            return Regex.IsMatch(email,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase);
        }
    }
}
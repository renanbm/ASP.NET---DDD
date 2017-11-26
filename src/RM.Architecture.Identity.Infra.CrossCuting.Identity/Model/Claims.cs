using System;
using System.ComponentModel.DataAnnotations;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.Model
{
    public class Claims
    {
        public Claims()
        {
            CodClaim = Guid.NewGuid();
        }

        public Guid CodClaim { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Forneça um nome para a Claim")]
        [MaxLength(128, ErrorMessage = "Tamanho máximo {0} excedido")]
        [Display(Name = "Nome da Claim")]
        public string Nome { get; set; }
    }
}
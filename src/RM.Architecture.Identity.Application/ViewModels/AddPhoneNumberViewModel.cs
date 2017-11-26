using System.ComponentModel.DataAnnotations;

namespace RM.Architecture.Identity.Application.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Número do Telefone")]
        public string Number { get; set; }
    }
}
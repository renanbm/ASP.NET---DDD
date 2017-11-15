using System.ComponentModel.DataAnnotations;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.Model
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}
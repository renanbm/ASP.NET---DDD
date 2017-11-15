using System.ComponentModel.DataAnnotations;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.Model
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace RM.Architecture.Identity.Application.ViewModels
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repita Nova Senha")]
        [Compare("NewPassword", ErrorMessage = "As senhas não se coincidem.")]
        public string ConfirmPassword { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace RM.Architecture.Identity.Application.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
using System.Collections.Generic;
using System.Web.Mvc;

namespace RM.Architecture.Identity.Application.ViewModels
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }

        [HiddenInput]
        public string UserId { get; set; }
    }
}
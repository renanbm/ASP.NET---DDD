using System.Collections.Generic;
using System.Web.Mvc;

namespace RM.Architecture.Identity.Application.ViewModels
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
    }
}
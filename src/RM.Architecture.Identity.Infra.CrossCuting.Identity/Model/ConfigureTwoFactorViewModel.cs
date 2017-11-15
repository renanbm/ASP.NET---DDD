using System.Collections.Generic;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.Model
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<string> Providers { get; set; }
    }
}
using System.Web.Mvc;
using RM.Architecture.Core.Infra.CrossCutting.MvcFilters;

namespace RM.Architecture.UI.Sistema
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new GlobalErrorHandler());
        }
    }
}
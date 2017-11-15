using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RM.Architecture.Filiacao.Application.AutoMapper;
using RM.Architecture.Filiacao.Infra.CrossCutting.BootStrapper;

namespace RM.Architecture.UI.Sistema
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();
            DapperMapping.RegisterDapperMappings();
        }
    }
}
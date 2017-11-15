using System.Web;
using System.Web.Http;
using RM.Architecture.Filiacao.Application.AutoMapper;

namespace RM.Architecture.Filiacao.Services.REST.FiliacaoAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.RegisterMappings();
        }
    }
}
[assembly: WebActivator.PostApplicationStartMethod(typeof(RM.Architecture.Filiacao.Services.REST.FiliacaoAPI.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace RM.Architecture.Filiacao.Services.REST.FiliacaoAPI.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using RM.Architecture.Filiacao.Infra.CrossCutting.BootStrapper;

    public static class SimpleInjectorWebApiInitializer
    {
        public static void Initialize()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new SimpleInjector.Lifestyles.AsyncScopedLifestyle();

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            SimpleInjectorMapping.Register(container);
        }
    }
}
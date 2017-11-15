using RM.Architecture.Filiacao.Application.Interfaces;
using RM.Architecture.Filiacao.Application.Services;
using RM.Architecture.Filiacao.Domain.Interfaces.Repository;
using RM.Architecture.Filiacao.Domain.Interfaces.Services;
using RM.Architecture.Filiacao.Domain.Services;
using RM.Architecture.Filiacao.Infrastructure.Data.Context;
using RM.Architecture.Filiacao.Infrastructure.Data.Repository;
using RM.Architecture.Filiacao.Infrastructure.Data.UnitOfWork;
using SimpleInjector;

namespace RM.Architecture.Filiacao.Infra.CrossCutting.BootStrapper
{
    public class SimpleInjectorMapping
    {
        // Lifestyle.Transient => uma instância para cada solicitação;
        // Lifesyle.Singleton => Uma instância única para a classe (para o servidor inteiro)
        // Lifestyle.Scoped => Uma instância única para o request

        public static void Register(Container container)
        {
            // APP
            container.Register<IFiliacaoAppService, FiliacaoAppService>(Lifestyle.Scoped);

            // Domain
            container.Register<IFiliacaoService, FiliacaoService>(Lifestyle.Scoped);

            // Data
            container.Register<IClienteRepository, ClienteRepository>(Lifestyle.Scoped);
            container.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<ArchitectureContext>(Lifestyle.Scoped);
        }
    }
}
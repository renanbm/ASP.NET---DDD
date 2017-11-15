using Dapper.FluentMap;
using RM.Architecture.Filiacao.Infrastructure.Data.DapperConfig;

namespace RM.Architecture.Filiacao.Infra.CrossCutting.BootStrapper
{
    public class DapperMapping
    {
        public static void RegisterDapperMappings()
        {
            FluentMapper.Initialize(config => { config.AddMap(new ClienteMap()); });
        }
    }
}
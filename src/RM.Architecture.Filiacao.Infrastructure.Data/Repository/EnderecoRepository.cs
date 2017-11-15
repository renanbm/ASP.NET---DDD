using RM.Architecture.Filiacao.Domain.Entities.Endereco;
using RM.Architecture.Filiacao.Domain.Interfaces.Repository;
using RM.Architecture.Filiacao.Infrastructure.Data.Context;

namespace RM.Architecture.Filiacao.Infrastructure.Data.Repository
{
    public class EnderecoRepository : BaseRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(ArchitectureContext context)
            : base(context)
        {
        }
    }
}
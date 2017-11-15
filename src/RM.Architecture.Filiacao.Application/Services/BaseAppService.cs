using RM.Architecture.Filiacao.Infrastructure.Data.UnitOfWork;

namespace RM.Architecture.Filiacao.Application.Services
{
    public class BaseAppService
    {
        private readonly IUnitOfWork _uow;

        public BaseAppService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected void Commit()
        {
            _uow.Commit();
        }
    }
}
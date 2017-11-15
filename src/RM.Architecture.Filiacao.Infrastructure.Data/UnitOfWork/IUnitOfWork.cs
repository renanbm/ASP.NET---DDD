using System;

namespace RM.Architecture.Filiacao.Infrastructure.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
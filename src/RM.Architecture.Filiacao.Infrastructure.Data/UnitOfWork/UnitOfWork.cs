using System;
using RM.Architecture.Filiacao.Infrastructure.Data.Context;

namespace RM.Architecture.Filiacao.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArchitectureContext _context;
        private bool _disposed;

        public UnitOfWork(ArchitectureContext context)
        {
            _context = context;
            _disposed = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }
    }
}
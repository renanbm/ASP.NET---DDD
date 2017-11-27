using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RM.Architecture.Filiacao.Domain.Interfaces.Repository;
using RM.Architecture.Filiacao.Infrastructure.Data.Context;

namespace RM.Architecture.Filiacao.Infrastructure.Data.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly ArchitectureContext Db;
        private readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(ArchitectureContext context)
        {
            Db = context;
            _dbSet = Db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Listar()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<TEntity> ListarPaginado(int take, int skip)
        {
            return _dbSet.Skip(skip).Take(take).ToList();
        }

        public IEnumerable<TEntity> Consultar(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public virtual TEntity Obter(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual TEntity Adicionar(TEntity obj)
        {
            return _dbSet.Add(obj);
        }

        public virtual TEntity Atualizar(TEntity obj)
        {
            _dbSet.Attach(obj);
            Db.Entry(obj).State = EntityState.Modified;

            return obj;
        }

        public virtual void Remover(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
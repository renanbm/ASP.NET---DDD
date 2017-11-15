using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RM.Architecture.Filiacao.Domain.Interfaces.Repository;
using RM.Architecture.Filiacao.Infrastructure.Data.Context;

namespace RM.Architecture.Filiacao.Infrastructure.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected ArchitectureContext Db;
        protected DbSet<TEntity> DbSet;

        public BaseRepository(ArchitectureContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Listar()
        {
            return DbSet.ToList();
        }

        public virtual IEnumerable<TEntity> ListarPaginado(int take, int skip)
        {
            return DbSet.Skip(skip).Take(take).ToList();
        }

        public IEnumerable<TEntity> Consultar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual TEntity Obter(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual TEntity Adicionar(TEntity obj)
        {
            return DbSet.Add(obj);
        }

        public virtual TEntity Atualizar(TEntity obj)
        {
            DbSet.Attach(obj);
            Db.Entry(obj).State = EntityState.Modified;

            return obj;
        }

        public virtual void Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
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
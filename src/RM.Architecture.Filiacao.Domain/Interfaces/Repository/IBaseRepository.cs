using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RM.Architecture.Filiacao.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> Listar();

        IEnumerable<TEntity> ListarPaginado(int take, int skip);

        IEnumerable<TEntity> Consultar(Expression<Func<TEntity, bool>> predicate);

        TEntity Obter(Guid id);

        TEntity Adicionar(TEntity obj);

        TEntity Atualizar(TEntity obj);

        void Remover(Guid id);

        int SaveChanges();
    }
}
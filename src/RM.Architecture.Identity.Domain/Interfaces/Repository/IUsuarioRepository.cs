using System;
using System.Collections.Generic;
using RM.Architecture.Identity.Domain.Entities;

namespace RM.Architecture.Identity.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IDisposable
    {
        Usuario ObterPorId(string id);
        IEnumerable<Usuario> ObterTodos();
        void DesativarLock(string id);
    }
}
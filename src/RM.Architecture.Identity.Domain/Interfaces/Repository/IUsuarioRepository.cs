using System;
using System.Collections.Generic;
using RM.Architecture.Identity.Domain.Entities;

namespace RM.Architecture.Identity.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IDisposable
    {
        Usuario ObterUsuario(string id);
        IEnumerable<Usuario> ListarUsuarios();
        void DesativarLock(string id);
    }
}
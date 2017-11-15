using System;
using System.Collections.Generic;
using System.Linq;
using RM.Architecture.Identity.Domain.Entities;
using RM.Architecture.Identity.Domain.Interfaces.Repository;
using RM.Architecture.Identity.Infra.Data.Context;

namespace RM.Architecture.Identity.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IdentityContext _db;

        public UsuarioRepository()
        {
            _db = new IdentityContext();
        }

        public Usuario ObterPorId(string id)
        {
            return _db.Usuarios.Find(id);
        }

        public IEnumerable<Usuario> ObterTodos()
        {
            return _db.Usuarios.ToList();
        }

        public void DesativarLock(string id)
        {
            _db.Usuarios.Find(id).LockoutEnabled = false;
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
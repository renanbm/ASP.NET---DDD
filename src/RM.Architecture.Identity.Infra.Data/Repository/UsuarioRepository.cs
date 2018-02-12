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
        #region [Variáveis Locais]

        private readonly IdentityContext _db;

        #endregion

        #region [Construtor]

        public UsuarioRepository()
        {
            _db = new IdentityContext();
        }

        #endregion

        #region [Obter]

        public Usuario ObterUsuario(string id)
        {
            return _db.Usuarios.Find(id);
        }

        #endregion

        #region [Listar]

        public IEnumerable<Usuario> ListarUsuarios()
        {
            return _db.Usuarios.ToList();
        }

        #endregion

        #region [Desativar Lock]

        public void DesativarLock(string id)
        {
            _db.Usuarios.Find(id).LockoutEnabled = false;
            _db.SaveChanges();
        }

        #endregion

        #region [Dispose]

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
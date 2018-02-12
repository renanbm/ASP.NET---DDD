using System;
using System.Collections.Generic;
using System.Linq;
using RM.Architecture.Identity.Domain.Entities;
using RM.Architecture.Identity.Domain.Interfaces.Repository;
using RM.Architecture.Identity.Infra.Data.Context;

namespace RM.Architecture.Identity.Infra.Data.Repository
{
    public class ClaimsRepository : IClaimsRepository
    {
        private readonly IdentityContext _db;

        public ClaimsRepository()
        {
            _db = new IdentityContext();
        }

        public List<Claims> Listar()
        {
            return _db.Claims.ToList();
        }

        public void Incluir(Claims claim)
        {
            _db.Claims.Add(claim);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
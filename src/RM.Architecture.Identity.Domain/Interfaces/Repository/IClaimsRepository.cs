using System;
using System.Collections.Generic;
using RM.Architecture.Identity.Domain.Entities;

namespace RM.Architecture.Identity.Domain.Interfaces.Repository
{
    public interface IClaimsRepository : IDisposable
    {
        List<Claims> Listar();

        void Incluir(Claims claim);
    }
}
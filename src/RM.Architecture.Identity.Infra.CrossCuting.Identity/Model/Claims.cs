using System;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.Model
{
    public class Claims
    {
        public Claims()
        {
            CodClaim = Guid.NewGuid();
        }

        public Guid CodClaim { get; private set; }

        public string Nome { get; set; }
    }
}
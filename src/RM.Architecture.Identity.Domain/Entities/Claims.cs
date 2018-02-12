using System;

namespace RM.Architecture.Identity.Domain.Entities
{
    public class Claims
    {
        public Claims()
        {
            CodClaim = Guid.NewGuid();
        }

        public Guid CodClaim { get; }

        public string Nome { get; set; }
    }
}
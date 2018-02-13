using System;
using System.ComponentModel.DataAnnotations;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.Model
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
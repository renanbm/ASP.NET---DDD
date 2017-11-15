using System;
using Microsoft.AspNet.Identity.EntityFramework;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.Identity.Infra.CrossCuting.Identity.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDisposable
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
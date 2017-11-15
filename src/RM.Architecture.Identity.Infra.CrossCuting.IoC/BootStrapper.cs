using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RM.Architecture.Identity.Domain.Interfaces.Repository;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Configuration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Context;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;
using RM.Architecture.Identity.Infra.Data.Repository;
using SimpleInjector;

namespace RM.Architecture.Identity.Infra.CrossCuting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser>>(
                () => new UserStore<ApplicationUser>(new ApplicationDbContext()), Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(), Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);

            container.Register<IUsuarioRepository, UsuarioRepository>(Lifestyle.Scoped);
        }
    }
}
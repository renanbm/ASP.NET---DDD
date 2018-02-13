using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Application.ViewModels;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.UI.Sistema.Controllers
{
    public class RoleController : Controller
    {
        private readonly IAuthorizationAppService _authorizationAppService;
        private readonly IUsuarioAppService _usuarioAppService;

        public RoleController(IAuthorizationAppService authorizationAppService, IUsuarioAppService usuarioAppService)
        {
            _authorizationAppService = authorizationAppService;
            _usuarioAppService = usuarioAppService;
        }

        public ActionResult Index()
        {
            var roles = _authorizationAppService.ListarRoles();
            return View(roles);
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var role = await _authorizationAppService.ObterRole(id);

            var users = new List<ApplicationUser>();

            var usuarios = _usuarioAppService.Listar();

            foreach (var user in usuarios)
                if (await _authorizationAppService.UsuarioPossuiRole(user.Id, role.Name))
                    users.Add(user);

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count;

            return View(role);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid) return View();

            var roleResult = await _authorizationAppService.IncluirRole(roleViewModel.Name);

            if (roleResult.Succeeded)
                return RedirectToAction("Index");

            ModelState.AddModelError("", roleResult.Errors.First());
            return View();
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var role = await _authorizationAppService.ObterRole(id);

            if (role == null)
                return HttpNotFound();

            var roleModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(roleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id")] RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid) return View();
            await _authorizationAppService.AtualizarRole(roleViewModel);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var role = await _authorizationAppService.ObterRole(id);
            if (role == null)
                return HttpNotFound();
            return View(role);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (!ModelState.IsValid) return View();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var role = await _authorizationAppService.ObterRole(id);

            if (role == null)
                return HttpNotFound();

            var result = await _authorizationAppService.Remover(role);
            
            if (result.Succeeded)
                return RedirectToAction("Index");

            ModelState.AddModelError("", result.Errors.First());
            return View();
        }
    }
}
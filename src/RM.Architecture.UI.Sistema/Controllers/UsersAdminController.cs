using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using RM.Architecture.Core.Infra.CrossCutting.MvcFilters;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Application.ViewModels;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.UI.Sistema.Controllers
{
    [ClaimsAuthorize("AdmUsers", "True")]
    public class UsersAdminController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly IAuthorizationAppService _authorizationAppService;

        public UsersAdminController(IUsuarioAppService usuarioAppService, IAuthorizationAppService authorizationAppService)
        {
            _authorizationAppService = authorizationAppService;
            _usuarioAppService = usuarioAppService;
        }

        public async Task<ActionResult> Index()
        {
            var usuarios = await _usuarioAppService.Listar();
            return View(usuarios);
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = await _usuarioAppService.ObterUsuario(id);

            ViewBag.RoleNames = await _authorizationAppService.ObterRolesUsuario(user.Id);
            ViewBag.Claims = await _authorizationAppService.ObterClaimsUsuario(user.Id);

            return View(user);
        }

        public async Task<ActionResult> Create()
        {
            var roles = await _authorizationAppService.ListarRoles();
            ViewBag.RoleId = new SelectList(roles, "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            var roles = await _authorizationAppService.ListarRoles();

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = userViewModel.Email, Email = userViewModel.Email};
                var adminResult = await _usuarioAppService.IncluirUsuario(user, userViewModel.Password);

                if (adminResult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await _authorizationAppService.AdicionarRoleUsuario(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await _authorizationAppService.ListarRoles(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminResult.Errors.First());
                    ViewBag.RoleId = new SelectList(roles, "Name", "Name");
                    return View();
                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(roles, "Name", "Name");
            return View();
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = await _usuarioAppService.ObterUsuario(id);
            if (user == null)
                return HttpNotFound();

            var userRoles = await _authorizationAppService.ObterRolesUsuario(user.Id);

            return View(new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                RolesList = _authorizationAppService.ListarRoles().Result.Select(x => new SelectListItem
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id")] EditUserViewModel editUser,
            params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await _usuarioAppService.ObterUsuario(editUser.Id);
                if (user == null)
                    return HttpNotFound();

                user.UserName = editUser.Email;
                user.Email = editUser.Email;

                var userRoles = await _authorizationAppService.ObterRolesUsuario(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await _authorizationAppService.AdicionarRoleUsuario(user.Id, selectedRole.Except(userRoles).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await _authorizationAppService.RemoverClaimsUsuario(user.Id, userRoles.Except(selectedRole).ToArray());

                if (result.Succeeded) return RedirectToAction("Index");

                ModelState.AddModelError("", result.Errors.First());
                return View();
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = await _usuarioAppService.ObterUsuario(id);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (!ModelState.IsValid) return View();

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = await _usuarioAppService.ObterUsuario(id);
            if (user == null)
                return HttpNotFound();

            var result = await _usuarioAppService.RemoverUsuario(user);

            if (result.Succeeded) return RedirectToAction("Index");

            ModelState.AddModelError("", result.Errors.First());

            return View();
        }
    }
}
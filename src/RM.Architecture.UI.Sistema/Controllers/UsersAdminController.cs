using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using RM.Architecture.Core.Infra.CrossCutting.MvcFilters;
using RM.Architecture.Identity.Application.ViewModels;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Configuration;
using RM.Architecture.Identity.Infra.CrossCuting.Identity.Model;

namespace RM.Architecture.UI.Sistema.Controllers
{
    [ClaimsAuthorize("AdmUsers", "True")]
    public class UsersAdminController : Controller
    {
        private readonly ApplicationRoleManager _roleManager;
        private readonly ApplicationUserManager _userManager;

        public UsersAdminController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        //
        // GET: /Users/
        public async Task<ActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = await _userManager.FindByIdAsync(id);

            ViewBag.RoleNames = await _userManager.GetRolesAsync(user.Id);
            ViewBag.Claims = await _userManager.GetClaimsAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = userViewModel.Email, Email = userViewModel.Email};
                var adminresult = await _userManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await _userManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
                    return View();
                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound();

            var userRoles = await _userManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                RolesList = _roleManager.Roles.ToList().Select(x => new SelectListItem
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id")] EditUserViewModel editUser,
            params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(editUser.Id);
                if (user == null)
                    return HttpNotFound();

                user.UserName = editUser.Email;
                user.Email = editUser.Email;

                var userRoles = await _userManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await _userManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await _userManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return HttpNotFound();
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
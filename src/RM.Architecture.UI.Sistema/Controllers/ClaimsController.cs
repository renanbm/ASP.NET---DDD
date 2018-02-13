using System.Web.Mvc;
using RM.Architecture.Core.Infra.CrossCutting.MvcFilters;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Application.ViewModels;

namespace RM.Architecture.UI.Sistema.Controllers
{
    [ClaimsAuthorize("AdmClaims", "True")]
    public class ClaimsController : Controller
    {
        private readonly IAuthorizationAppService _authorizationAppService;
        private readonly IUsuarioAppService _usuarioAppService;

        public ClaimsController(IAuthorizationAppService authorizationAppService, IUsuarioAppService usuarioAppService)
        {
            _authorizationAppService = authorizationAppService;
            _usuarioAppService = usuarioAppService;
        }

        public ActionResult Index()
        {
            var claims = _authorizationAppService.ListarClaims();
            return View(claims);
        }

        public ActionResult IncluirClaimUsuario(string id)
        {
            ViewBag.Type = new SelectList
            (
                _authorizationAppService.ListarClaims(),
                "Name",
                "Name"
            );

            ViewBag.User = _usuarioAppService.ObterUsuario(id);

            return View();
        }

        [HttpPost]
        public ActionResult IncluirClaimUsuario(ClaimViewModel claim, string id)
        {
            try
            {
                _authorizationAppService.IncluirClaimUsuario(id, claim);

                return RedirectToAction("Details", "UsersAdmin", new { id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult IncluirClaim()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IncluirClaim(ClaimViewModel claim)
        {
            try
            {
                if (ModelState.IsValid)
                    _authorizationAppService.IncluirClaim(claim);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
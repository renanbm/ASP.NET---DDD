using System.Web.Mvc;
using RM.Architecture.Core.Infra.CrossCutting.MvcFilters;
using RM.Architecture.Identity.Application.Interfaces;
using RM.Architecture.Identity.Application.ViewModels;

namespace RM.Architecture.UI.Sistema.Controllers
{
    [ClaimsAuthorize("AdmClaims", "True")]
    public class ClaimsAdminController : Controller
    {
        private readonly IClaimsAppService _claimsAppService;
        private readonly IUsuarioAppService _usuarioAppService;

        public ClaimsAdminController(IClaimsAppService claimsAppService, IUsuarioAppService usuarioAppService)
        {
            _claimsAppService = claimsAppService;
            _usuarioAppService = usuarioAppService;
        }

        public ActionResult Index()
        {
            var claims = _claimsAppService.ListarClaims();
            return View(claims);
        }

        public ActionResult IncluirClaimUsuario(string id)
        {
            ViewBag.Type = new SelectList
            (
                _claimsAppService.ListarClaims(),
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
                _claimsAppService.IncluirClaimUsuario(id, claim);

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
                    _claimsAppService.IncluirClaim(claim);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
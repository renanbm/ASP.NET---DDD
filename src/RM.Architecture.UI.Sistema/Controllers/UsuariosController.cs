using System.Web.Mvc;
using RM.Architecture.Identity.Application.Interfaces;

namespace RM.Architecture.UI.Sistema.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuariosController(IUsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        public ActionResult Index()
        {
            var usuarios = _usuarioAppService.ListarUsuarios();
            return View(usuarios);
        }

        public ActionResult Details(string id)
        {
            var usuario = _usuarioAppService.ObterUsuarioRepo(id);
            return View(usuario);
        }

        public ActionResult DesativarLock(string id)
        {
            _usuarioAppService.DesativarLock(id);
            return RedirectToAction("Index");
        }
    }
}
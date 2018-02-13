using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RM.Architecture.Core.Infra.CrossCutting.MvcFilters;
using RM.Architecture.Filiacao.Application.Interfaces;
using RM.Architecture.Filiacao.Application.ViewModels;

namespace RM.Architecture.UI.Sistema.Controllers
{
    [Authorize]
    [RoutePrefix("administrativo-filiacao")]
    public class FiliacaoController : Controller
    {
        private readonly IFiliacaoAppService _filiacaoAppService;

        public FiliacaoController(IFiliacaoAppService filiacaoAppService)
        {
            _filiacaoAppService = filiacaoAppService;
        }

        [Route("listar-clientes")]
        [ClaimsAuthorize("ModuloFiliacao", "FL")]
        public ActionResult Index()
        {
            return View(_filiacaoAppService.Listar().ToList());
        }

        [Route("{id:guid}/detalhe-cliente")]
        [ClaimsAuthorize("ModuloFiliacao", "FD")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var clienteViewModel = _filiacaoAppService.Obter(id.Value);

            if (clienteViewModel == null)
                return HttpNotFound();

            return View(clienteViewModel);
        }

        [Route("novo-cliente")]
        [ClaimsAuthorize("ModuloFiliacao", "FI")]
        public ActionResult Create()
        {
            return View();
        }

        [Route("novo-cliente")]
        [ClaimsAuthorize("ModuloFiliacao", "FI")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteEnderecoViewModel clienteEnderecoViewModel)
        {
            if (!ModelState.IsValid) return View(clienteEnderecoViewModel);

            var clienteReturn = _filiacaoAppService.Adicionar(clienteEnderecoViewModel).ClienteViewModel;
            if (clienteReturn.ValidationResult.IsValid) return RedirectToAction("Index");

            foreach (var erro in clienteReturn.ValidationResult.Erros)
                ModelState.AddModelError(string.Empty, erro.Message);

            return View(clienteEnderecoViewModel);
        }

        [Route("{id:guid}/editar-cliente")]
        [ClaimsAuthorize("ModuloFiliacao", "FE")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var clienteViewModel = _filiacaoAppService.Obter(id.Value);

            if (clienteViewModel == null)
                return HttpNotFound();

            return View(clienteViewModel);
        }

        [Route("{id:guid}/editar-cliente")]
        [ClaimsAuthorize("ModuloFiliacao", "FE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid) return View(clienteViewModel);
            _filiacaoAppService.Atualizar(clienteViewModel);
            return RedirectToAction("Index");
        }

        [Route("{id:guid}/excluir-cliente")]
        [ClaimsAuthorize("ModuloFiliacao", "FX")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var clienteViewModel = _filiacaoAppService.Obter(id.Value);

            if (clienteViewModel == null)
                return HttpNotFound();

            return View(clienteViewModel);
        }

        [Route("{id:guid}/excluir-cliente")]
        [ClaimsAuthorize("ModuloFiliacao", "FX")]
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _filiacaoAppService.Remover(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _filiacaoAppService.Dispose();
            base.Dispose(disposing);
        }
    }
}
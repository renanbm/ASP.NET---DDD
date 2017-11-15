using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using RM.Architecture.Filiacao.Application.Interfaces;
using RM.Architecture.Filiacao.Application.ViewModels;

namespace RM.Architecture.Filiacao.Services.REST.FiliacaoAPI.Controllers
{
    [EnableCors("*", "*", "GET")]
    public class FiliacaoController : ApiController
    {
        private readonly IFiliacaoAppService _filiacaoAppService;

        public FiliacaoController(IFiliacaoAppService filiacaoAppService)
        {
            _filiacaoAppService = filiacaoAppService;
        }

        public IEnumerable<ClienteViewModel> Get()
        {
            return _filiacaoAppService.Listar();
        }

        public ClienteViewModel Get(Guid id)
        {
            return _filiacaoAppService.Obter(id);
        }

        public void Post([FromBody] string value)
        {
            //_filiacaoAppService.Adicionar();
        }

        public void Put(int id, [FromBody] string value)
        {
            //_filiacaoAppService.Atualizar();
        }

        public void Delete(Guid id)
        {
            _filiacaoAppService.Remover(id);
        }
    }
}
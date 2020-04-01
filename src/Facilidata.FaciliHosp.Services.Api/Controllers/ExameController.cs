using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facilidata.FaciliHosp.Services.Api.Controllers
{
    public class ExameController : BaseController
    {
        private readonly IExameRepository _exameRepository;

        public ExameController(IUnitOfWork<ContextSQL> uow, IExameRepository exameRepository) : base(uow)
        {
            _exameRepository = exameRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetObterTodos()
        {
            var usuario = HttpContext.User;
            var hospitais = _exameRepository.ObterTodos();
            return Resposta(hospitais);
        }

        [HttpGet("{id}")]
        public IActionResult GetObterPorId(string id)
        {
            var Exame = _exameRepository.ObterPorId(id);
            return Resposta(Exame);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Exame exame)
        {
            _exameRepository.Inserir(exame);
            Commit();
            return Resposta();
        }

        [HttpPut]
        public IActionResult Put(string id, [FromBody] Exame exame)
        {
            _exameRepository.Atualizar(id, exame);
            Commit();
            return Resposta();
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            _exameRepository.Deletar(id);
            Commit();
            return Resposta();

        }
    }
}

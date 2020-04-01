using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facilidata.FaciliHosp.Services.Api.Controllers
{
    [AllowAnonymous]
    public class ExameController : BaseController
    {
        private readonly IExameRepository _exameRepository;

        public ExameController(IUnitOfWork<ContextSQL> uow, IExameRepository exameRepository) : base(uow)
        {
            _exameRepository = exameRepository;
        }

        [HttpGet]
        public IActionResult GetObterTodos(string hospitaId, string usuarioId)
        {
            var hospitais = _exameRepository.ObterTodosSemAnexoPorHospitalIdEUsuarioId(hospitaId, usuarioId);
            return Resposta(hospitais);
        }


        [HttpGet("anexo")]
        public IActionResult GetAnexosPorId(string id)
        {
            var exame = _exameRepository.ObterPorId(id);
            var obj = new { Anexo = exame.Anexo, ContentType = exame.ContentType, NomeArquivo = exame.NomeArquivo };
            return Resposta(obj);
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

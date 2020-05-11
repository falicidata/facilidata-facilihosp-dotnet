using Facilidata.FaciliHosp.Application.Interfaces;
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
        private readonly IAzureStorageService _azureStorageService;

        public ExameController(IUnitOfWork<ContextSQL> uow, IExameRepository exameRepository, IAzureStorageService azureStorageService) : base(uow)
        {
            _exameRepository = exameRepository;
            _azureStorageService = azureStorageService;
        }

        //[HttpGet]
        //public IActionResult GetObterTodos(string hospitaId, string usuarioId)
        //{
        //    var hospitais = _exameRepository.ObterTodosSemAnexoPorHospitalIdEUsuarioId(hospitaId, usuarioId);
        //    return Resposta(hospitais);
        //}


        [HttpGet("anexo")]
        public IActionResult GetAnexoPorId(string id)
        {
            var exame = _exameRepository.ObterPorId(id);
            string base64 = _azureStorageService.DownloadToBase64(exame.Url);
            var obj = new { Base64 = base64, ContentType = exame.ContentType, NomeArquivo = exame.NomeArquivo };
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

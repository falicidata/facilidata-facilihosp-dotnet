using Facilidata.FaciliHosp.Application.Interfaces;
using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Presentation.Site.Controllers.API
{

    public class ExameController : BaseApiController
    {
        private readonly IExameRepository _exameRepository;
        private readonly IExameService _exameService;
        private readonly IAzureStorageService _azureStorageService;

        public ExameController(IUnitOfWork<ContextSQL> uow, IExameRepository exameRepository, IAzureStorageService azureStorageService, IExameService exameService) : base(uow)
        {
            _exameRepository = exameRepository;
            _azureStorageService = azureStorageService;
            _exameService = exameService;
        }

        [HttpGet]
        public IActionResult GetObterTodos()
        {
            var exames = _exameService.ObterExamesUsuarioLogado();
            return Resposta(exames);
        }

        [HttpGet("anexo-download")]
        public IActionResult DownloadAnexoApi(string id)
        {
            var exame = _exameRepository.ObterPorId(id);
            if (string.IsNullOrEmpty(exame.Url)) return null;
            var arraybyte = _azureStorageService.DownloadToBytes(exame.Url);
            if (arraybyte == null) return null;
            return File(arraybyte, exame.ContentType, exame.NomeArquivo);
        }



        //[HttpGet("anexo")]
        //public IActionResult GetAnexoPorId(string id)
        //{
        //    var exame = _exameRepository.ObterPorId(id);
        //    string base64 = _azureStorageService.DownloadToBase64(exame.Url);
        //    var obj = new { Base64 = base64, ContentType = exame.ContentType, NomeArquivo = exame.NomeArquivo };
        //    return Resposta(obj);
        //}

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

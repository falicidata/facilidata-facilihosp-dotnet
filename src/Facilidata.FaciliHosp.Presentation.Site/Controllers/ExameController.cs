using AutoMapper;
using Facilidata.FaciliHosp.Application.Interfaces;
using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Presentation.Site.Controllers
{
  
    public class ExameController : Controller
    {
        private readonly IExameService _exameService;
        private readonly IExameRepository _exameRepository;
        private readonly IAzureStorageService _azureStorageService;
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioAspNet _usuarioAspNet;

        public ExameController(IExameService exameService, IExameRepository exameRepository, IAzureStorageService azureStorageService, IUsuarioService usuarioService, IUsuarioAspNet usuarioAspNet)
        {
            _exameService = exameService;
            _exameRepository = exameRepository;
            _azureStorageService = azureStorageService;
            _usuarioService = usuarioService;
            _usuarioAspNet = usuarioAspNet;
        }



        public IActionResult Editar(string id)
        {

            var viewModel = _exameService.Editar(id);
            if (viewModel == null) return RedirectToAction("Index", new { });
            return View(viewModel);

        }


        public IActionResult RemoverAnexo(EditarExameViewModel viewModel)
        {

            if (!ModelState.IsValid) View("Editar", viewModel);
            _exameService.RemoverAnexo(viewModel);
            return RedirectToAction("Editar", new { Id = viewModel.Id });
        }

        public object DownloadAnexo(string id)
        {
            var exame = _exameRepository.ObterPorId(id);
            if (string.IsNullOrEmpty(exame.Url)) return null;
            var arraybyte = _azureStorageService.DownloadToBytes(exame.Url);
            if (arraybyte == null) return null;
            return File(arraybyte, exame.ContentType, exame.NomeArquivo);
        }

       
        [HttpPost]
        public IActionResult Salvar(EditarExameViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("Editar", viewModel);
            var resultado = _exameService.Salvar(viewModel);
            if (resultado) return RedirectToAction("Index");
            return View("Editar", viewModel);
        }

        public IActionResult Index()
        {
            return View(_exameService.ObterExamesUsuarioLogado());
        }

        public IActionResult Deletar(string id)
        {
            _exameService.Deletar(id);
            return RedirectToAction("Index");
        }
    }



}

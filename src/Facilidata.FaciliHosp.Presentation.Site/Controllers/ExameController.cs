using AutoMapper;
using Facilidata.FaciliHosp.Application.Interfaces;
using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Enums;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using iTextSharp.text;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Presentation.Site.Controllers
{

    public class ExameController : Controller
    {
        private readonly IExameService _exameService;
        private readonly IExameCompRepository _exameCompRepository;
        private readonly IExameRepository _exameRepository;
        private readonly IAzureStorageService _azureStorageService;
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioAspNet _usuarioAspNet;

        public ExameController(IExameService exameService, IExameRepository exameRepository, IAzureStorageService azureStorageService, IUsuarioService usuarioService, IUsuarioAspNet usuarioAspNet, IExameCompRepository exameCompRepository)
        {
            _exameService = exameService;
            _exameRepository = exameRepository;
            _azureStorageService = azureStorageService;
            _usuarioService = usuarioService;
            _usuarioAspNet = usuarioAspNet;
            _exameCompRepository = exameCompRepository;
        }

        public IActionResult VisualizarCompartilhado(string id)
        {
            var viewModel = _exameService.Editar(id);
            if (viewModel == null) return RedirectToAction("Index", new { });
            return View(viewModel);

        }

        public IActionResult Editar(string id)
        {

            var viewModel = _exameService.Editar(id);
            if (viewModel == null) return RedirectToAction("Index", new { });
            return View(viewModel);

        }

        public IActionResult Retorno(string id)
        {
            var viewModel = _exameService.Editar(id);
            if (viewModel == null) return RedirectToAction("Index", new { });
            return View(new RetornoViewModel() { ExameId = viewModel.Id, ResultadoAvaliacao = viewModel.ResultadoAvaliacao, Retorno = viewModel.Retorno, RetornoUsuario = viewModel.RetornoUsuario });
        }

        public IActionResult CompartilhadosComUsuario()
        {
            var exameComps = _exameService.ObterCompartilhadosPorUsuarioCompartilhado();
            return View(exameComps);
        }

        public IActionResult Compartilhados()
        {
            var exameComps = _exameService.ObterCompartilhados();
            return View(exameComps);
        }

        public IActionResult RemoverCompartilhado(string id)
        {

            var res = _exameService.RemoverCompartilhado(id);
            return View("Compartilhados",_exameService.ObterCompartilhados());
        }

        public IActionResult Compartilhado(string id)
        {
            var exameComp = _exameCompRepository.ObterPorKey(id);
            var res = _exameService.CompartilharExame(id);
            var viewModel = _exameService.Editar(exameComp.ExameId);
            if (res) return View("Editar", viewModel);
            return RedirectToAction("Index", new { });
        }

        public IActionResult Compartilhar(string id)
        {
            string key = Guid.NewGuid().ToString();
            string port = !HttpContext.Request.Host.Port.HasValue ? "" : $":{HttpContext.Request.Host.Port}";
            string host = $"{HttpContext.Request.Host.Host}{port}";
            string path = $"{host}/exame/compartilhado";
            var viewModel = new ExameCompViewModel() { ExameId = id, Key = key, Url = path, Periodo = Domain.Enums.EPeriodoComp.Hora };
            return View(viewModel);
        }

        public IActionResult CompartilharQrCode(string exameId, EPeriodoComp periodo)
        {

            var key = _exameService.GerarCodigoComp(exameId, periodo);
            string port = !HttpContext.Request.Host.Port.HasValue ? "" : $":{HttpContext.Request.Host.Port}";
            string host = $"{HttpContext.Request.Host.Host}{port}";
            string path = $"{host}/exame/compartilhado/{key}";
            return View(new ExameCompViewModel { ExameId = exameId, Periodo = periodo, Key = key, Url = path });
        }

        public IActionResult SalvarRetorno(RetornoViewModel viewModel)
        {
            _exameService.AdicionarRetorno(viewModel.ExameId, viewModel.ResultadoAvaliacao, viewModel.Retorno);
            if (ModelState.IsValid) return RedirectToAction("Index", new { });
            return View("Retorno", viewModel);
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
            var exames = _exameService.ObterExamesUsuarioLogado();
            return View(exames.OrderByDescending(exame => exame.CriadoEm).ToList());
        }

        public IActionResult Deletar(string id)
        {
            _exameService.Deletar(id);
            return RedirectToAction("Index");
        }

        public IActionResult Tipos()
        {
            var tipos = _exameService.ObterExamesTipos();
            return Json(tipos);
        }
    }



}

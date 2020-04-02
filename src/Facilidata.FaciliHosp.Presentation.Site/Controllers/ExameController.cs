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
        private readonly IHospitalRepository _hospitalRepository;

        public ExameController(IExameService exameService, IExameRepository exameRepository, IAzureStorageService azureStorageService, IUsuarioService usuarioService, IUsuarioAspNet usuarioAspNet, IHospitalRepository hospitalRepository)
        {

            _exameService = exameService;
            _exameRepository = exameRepository;
            _azureStorageService = azureStorageService;
            _usuarioService = usuarioService;
            _usuarioAspNet = usuarioAspNet;
            _hospitalRepository = hospitalRepository;
        }


        public IActionResult NovoExameHospitais(string usuarioId)
        {
            var hospitais = _hospitalRepository.ObterTodos();

            return View(new NovoExameHospitalIndexViewModel { UsuarioId = usuarioId, Hospitais = hospitais });
        }
        public IActionResult Pacientes(string hospitalId)
        {

            return View(_exameService.ObterPacientes(hospitalId));
        }

        public IActionResult Editar(string id, string hospitalId, string usuarioId)
        {

            var viewModel = _exameService.Editar(id, hospitalId, usuarioId);
            if (viewModel == null) return RedirectToAction("Index", new { HospitalId = hospitalId, UsuarioId = usuarioId });
            return View(viewModel);

        }

        public IActionResult RemoverAnexo(EditarExameViewModel viewModel)
        {
            _exameService.RemoverAnexo(viewModel);
            if (!ModelState.IsValid) View("Editar", viewModel);
            viewModel.ContentType = null;
            viewModel.NomeArquivo = null;
            return View("Editar", viewModel);
        }

        public object DownloadAnexo(string id)
        {
            var exame = _exameRepository.ObterPorId(id);
            if (string.IsNullOrEmpty(exame.Url)) return null;
            var arraybyte = _azureStorageService.DownloadToBytes(exame.Url);
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
            var tipoUsuario = _usuarioService.GetTipoUsuarioLogado();
            var usuarioId = _usuarioAspNet.GetUsuarioId();

            if (tipoUsuario == Infra.Identity.Enums.ETipoUsuario.Medico)
                return View(_exameRepository.ObterTodosSemAnexoComHospitalEUsuario());
            else
                return View(_exameRepository.ObterTodosSemAnexoComHospitalEUsuarioPorUsuarioId(usuarioId));
        }

        public IActionResult Deletar(string id, string hospitalId, string usuarioId)
        {
            _exameService.Deletar(id);
            return RedirectToAction("Index", new { HospitalId = hospitalId, UsuarioId = usuarioId });
        }
    }



}

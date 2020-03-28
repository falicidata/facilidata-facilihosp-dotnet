using AutoMapper;
using Facilidata.FaciliHosp.Application.Interfaces;
using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Presentation.Site.Controllers
{

    public class HospitalController : Controller
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IHospitalService _hospitalService;
        private readonly IMapper _mapper;

        private readonly IUnitOfWork<ContextSQL> _uow;

        public HospitalController(IHospitalRepository hospitalRepository, IUnitOfWork<ContextSQL> uow, IHospitalService hospitalService, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _uow = uow;
            _hospitalService = hospitalService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {

            var hospitais = _hospitalRepository.ObterTodos();
            return View(hospitais);

        }

        public IActionResult Editar(string id = null)
        {
            if (id == null) return View(new EditarHospitalViewModel());
            var hospital = _hospitalRepository.ObterPorId(id);
            if (hospital == null) return View("Index");
            var viewModel = _mapper.Map<EditarHospitalViewModel>(hospital);
            return View(viewModel);
        }

        public IActionResult Deletar(string id)
        {
            _hospitalRepository.Deletar(id);
            var res = _uow.Commit();
            if (res == true) return RedirectToAction("Index");
            else return View("Index");
        }

        public IActionResult Salvar(EditarHospitalViewModel viewModel)
        {
            if (!this.ModelState.IsValid) return View("Editar", viewModel);
            var resultado = _hospitalService.Salvar(viewModel);
            if (resultado == true) return RedirectToAction("Index");
            else return View("Editar", viewModel);

        }
    }
}

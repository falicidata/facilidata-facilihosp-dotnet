using AutoMapper;
using Facilidata.FaciliHosp.Application.Interfaces;
using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facilidata.FaciliHosp.Application.Services
{
   public class ExameService : Service, IExameService
    {
        private readonly IExameRepository _exameRepository;
        private readonly IUsuarioService _usuarioService;

        public ExameService(IUnitOfWork<ContextSQLS> uow, IMapper mapper, IExameRepository exameRepository, IUsuarioService usuarioService) : base(uow, mapper)
        {
            _exameRepository = exameRepository;
            _usuarioService = usuarioService;
        }

        public bool Deletar(string id)
        {
            _exameRepository.Deletar(id);
            return _uow.Commit();
        }

        public EditarExameViewModel Editar(string id,string hospitalId,string usuarioId)
        {
            if (string.IsNullOrEmpty(id))
                return new EditarExameViewModel() { HospitalId = hospitalId, UsuarioId = usuarioId };

            var exame = _exameRepository.ObterPorId(id);
            if (exame == null) return null;
            var viewModel = _mapper.Map<EditarExameViewModel>(exame);
            return viewModel;
        }

        public ExameIndexViewModel ObterExamesPorHospitaIdEUsuarioId(string hospitalId, string usuarioId)
        {
            var exames = _exameRepository.ObterTodosPorHospitalIdPorUsuarioId(hospitalId, usuarioId);
            var viewModel = new ExameIndexViewModel() { Exames = exames, HospitalId = hospitalId, UsuarioId = usuarioId };
            return viewModel;
        }

        public List<ExamePacientesViewModel> ObterPacientes(string hospitalId)
        {
            var pacientes = _usuarioService.ObterPacientes();
            var viewModel = pacientes.Select(paciente => new ExamePacientesViewModel() { HospitalId = hospitalId, Nome = paciente.Usuario.UserName, Id = paciente.Usuario.Id }).ToList();
            return viewModel;
        }

        public bool Salvar(EditarExameViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Id))
            {
                var novoExame = _mapper.Map<Exame>(viewModel);
                _exameRepository.Inserir(novoExame);
                var novoResultado = _uow.Commit();
                return novoResultado;
            }

            var exame = _exameRepository.ObterPorId(viewModel.Id);
            if (exame == null) return false;
            var exameAtualizado = _mapper.Map<Exame>(viewModel);
            _exameRepository.Atualizar(viewModel.Id, exameAtualizado);
            var atualizacaoResultado = _uow.Commit();
            return atualizacaoResultado;
        }
    }
}

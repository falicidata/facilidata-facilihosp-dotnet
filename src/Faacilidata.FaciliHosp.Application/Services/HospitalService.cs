using AutoMapper;
using Facilidata.FaciliHosp.Application.Interfaces;
using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facilidata.FaciliHosp.Application.Services
{
    public class HospitalService : Service,IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;
        public HospitalService(IUnitOfWork<ContextSQLS> uow, IMapper mapper, IHospitalRepository hospitalRepository) : base(uow, mapper)
        {
            _hospitalRepository = hospitalRepository;
        }

        public List<Hospital> ObterTodos() => _hospitalRepository.ObterTodos();

        public bool Salvar(EditarHospitalViewModel viewModel)
        {
            Hospital hospital;
            if (string.IsNullOrEmpty(viewModel.Id))
            {
                hospital = _mapper.Map<Hospital>(viewModel);
                _hospitalRepository.Inserir(hospital);
                return _uow.Commit();

            }

            hospital = _hospitalRepository.ObterPorId(viewModel.Id);

            _hospitalRepository.Atualizar(viewModel.Id, _mapper.Map<Hospital>(viewModel));
            return _uow.Commit();
        }

    }
}

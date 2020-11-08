using AutoMapper;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Enums;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Infra.Identity.Services
{
    public class PlanoService :  IPlanoService
    {
        private readonly IPlanoRepository _planoRepository;
        protected readonly IMapper _mapper;

        public PlanoService(IPlanoRepository planoRepository, IMapper mapper)
        {
            _planoRepository = planoRepository;
            _mapper = mapper;
        }



        public List<Plano> ObterPlanos()
        {
            var planos = _planoRepository.ObterTodos().OrderBy(x => x.Valor).ToList();
            if (planos == null) return null;
            return planos;
        }

    }
}

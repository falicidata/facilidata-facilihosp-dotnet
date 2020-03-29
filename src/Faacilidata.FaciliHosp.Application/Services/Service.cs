using AutoMapper;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Facilidata.FaciliHosp.Application.Services
{
    public abstract class Service
    {
        protected readonly IUnitOfWork<ContextSQL> _uow;
        protected readonly IMapper _mapper;
        protected readonly IActionContextAccessor _actionContextAccessor;
        protected Service(IUnitOfWork<ContextSQL> uow, IMapper mapper, IActionContextAccessor actionContextAccessor)
        {
            _uow = uow;
            _mapper = mapper;
            _actionContextAccessor = actionContextAccessor;
        }


        protected void AdicionarErroModelState(string erro, string key = null)
        {
            _actionContextAccessor.ActionContext.ModelState.AddModelError(string.IsNullOrEmpty(key) ? "" : key, erro);
        }

        public bool Commit()
        {

            bool resultado = _uow.Commit();
            if (resultado == true)
            {
                return true;
            }
            else
            {
                AdicionarErroModelState("Ocorreu um erro ao atualizar o banco de dados", "Commit");
                return false;
            }
        }
    }
}

using AutoMapper;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;

namespace Facilidata.FaciliHosp.Application.Services
{
    public abstract  class Service
    {
        protected readonly IUnitOfWork<ContextSQLS> _uow;
        protected readonly IMapper _mapper;

        protected Service(IUnitOfWork<ContextSQLS> uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
    }
}

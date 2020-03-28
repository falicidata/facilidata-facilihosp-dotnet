using AutoMapper;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;

namespace Facilidata.FaciliHosp.Application.Services
{
    public abstract  class Service
    {
        protected readonly IUnitOfWork<ContextSQL> _uow;
        protected readonly IMapper _mapper;

        protected Service(IUnitOfWork<ContextSQL> uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
    }
}

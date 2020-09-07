using AutoMapper;
using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;

namespace Facilidata.FaciliHosp.Application.AutoMapperProfiles
{
    public  class ViewModelToModel : Profile
    {
        public ViewModelToModel()
        {
            this.CreateMap<EditarExameViewModel, Exame>();
            this.CreateMap<EditarExameViewModel, Exame>().ReverseMap();
            this.CreateMap<RegistroViewModel, Conta>().ReverseMap();
            this.CreateMap<RegistroViewModel, Conta>().ReverseMap();
            this.CreateMap<AlteracaoViewModel, Conta>();
            this.CreateMap<AlteracaoViewModel, Conta>().ReverseMap();
        }
    }
}

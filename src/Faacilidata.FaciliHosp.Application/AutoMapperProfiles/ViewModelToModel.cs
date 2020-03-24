using AutoMapper;
using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Entidades;

namespace Facilidata.FaciliHosp.Application.AutoMapperProfiles
{
    public  class ViewModelToModel : Profile
    {
        public ViewModelToModel()
        {
            this.CreateMap<EditarHospitalViewModel, Hospital>();
            this.CreateMap<EditarHospitalViewModel, Hospital>().ReverseMap();
        }
    }
}

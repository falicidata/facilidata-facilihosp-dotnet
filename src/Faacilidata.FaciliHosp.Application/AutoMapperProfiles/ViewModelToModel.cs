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
            this.CreateMap<EditarHospitalViewModel, Hospital>();
            this.CreateMap<EditarHospitalViewModel, Hospital>().ReverseMap();
            this.CreateMap<EditarExameViewModel, Exame>();
            this.CreateMap<EditarExameViewModel, Exame>().ReverseMap();
            this.CreateMap<RegistroPacienteViewModel, Paciente>().ReverseMap();
            this.CreateMap<RegistroPacienteViewModel, Paciente>().ReverseMap();
            this.CreateMap<RegistroMedicoViewModel, Medico>().ReverseMap();
            this.CreateMap<RegistroMedicoViewModel, Medico>().ReverseMap();
        }
    }
}

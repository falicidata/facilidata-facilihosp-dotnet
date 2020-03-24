using Facilidata.FaciliHosp.Application.ViewModels;

namespace Facilidata.FaciliHosp.Application.Interfaces
{
    public interface IHospitalService
    {
        bool Salvar(EditarHospitalViewModel viewModel);
    }
}

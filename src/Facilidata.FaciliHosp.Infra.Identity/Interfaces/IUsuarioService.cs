using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Infra.Identity.Interfaces
{
    public interface IUsuarioService
    {
        Task<IdentityResult> Registro(RegistroViewModel viewModel);
        Task<bool> Login(LoginUsuarioViewModel viewModel);
        Task Logout();

    }
}

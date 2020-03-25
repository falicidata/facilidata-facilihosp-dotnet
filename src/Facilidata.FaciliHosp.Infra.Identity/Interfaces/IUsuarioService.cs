using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Infra.Identity.Interfaces
{
    public interface IUsuarioService
    {
        List<Paciente> ObterPacientes();
        Task<IdentityResult> Registro(RegistroUsuarioViewModel viewModel);
        Task<bool> Login(LoginUsuarioViewModel viewModel);
        Task Logout();

    }
}

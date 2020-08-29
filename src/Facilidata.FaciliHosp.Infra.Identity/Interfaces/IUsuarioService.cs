using Facilidata.FaciliHosp.Infra.Identity.Models;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Infra.Identity.Interfaces
{
    public interface IUsuarioService
    {
        List<Usuario> ObterTodos();
        Task<IdentityResult> Registro(RegistroViewModel viewModel);
        Task<bool> Login(LoginUsuarioViewModel viewModel);
        Task Logout();
        Usuario ObterPorId();

    }
}

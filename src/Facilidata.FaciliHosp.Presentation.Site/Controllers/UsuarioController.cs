using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Presentation.Site.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Route("/registro")]
        public IActionResult Registro()
        {
            return View();
        }

 
        public async Task<IActionResult> Registrar(RegistroViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("Registro", viewModel);
            var res = await _usuarioService.Registro(viewModel);
            if (res.Succeeded) return RedirectToAction("Login");
            return View("Registro");
        }

        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await _usuarioService.Logout();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> EnviarLogin(LoginUsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("Login",viewModel);
            var res = await _usuarioService.Login(viewModel);
            if (res == true)
            {
                return RedirectToAction("IndexUsuario", "Home");
            }
            else
            {
                ModelState.AddModelError("Login", "Email ou Senha incorretos");
                return View("Login");
            }
        }
    }
}

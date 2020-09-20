using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Presentation.Site.Controllers
{
    [AllowAnonymous]
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
            if (res != null && res.Succeeded)
            {
                var loginUsuarioViewModel = new LoginUsuarioViewModel();
                loginUsuarioViewModel.Email = viewModel.Email;
                loginUsuarioViewModel.Senha = viewModel.Senha;
                var user = await _usuarioService.Login(loginUsuarioViewModel);
                if (user == true)
                {
                    return RedirectToAction("IndexUsuario", "Home");
                }
            }
            return View("Registro");
        }

        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

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

        [Route("/alteracao")]
        public IActionResult Alteracao()
        {
            var viewModel = _usuarioService.ObterPorId();
            return View(viewModel);
        }

        public async Task<IActionResult> Salvar(AlteracaoViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("Alteracao", viewModel);
            var res = await _usuarioService.Salvar(viewModel);
            if (res)
            {
                return RedirectToAction("IndexUsuario", "Home");
            }
            return View("Alteracao");
        }



    }
}

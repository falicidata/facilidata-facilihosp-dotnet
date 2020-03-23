using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IActionResult> Registrar(RegistroUsuarioViewModel viewModel)
        {
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
            var res = await _usuarioService.Login(viewModel);
            var auth = HttpContext.User.Identity.IsAuthenticated;
            if (res == true) return RedirectToAction("Index", "Home");
            else return View("Login");
        }
    }
}

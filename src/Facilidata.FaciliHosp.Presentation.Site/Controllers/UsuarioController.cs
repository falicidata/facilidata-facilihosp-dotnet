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

        public async Task<IActionResult> Registrar(RegistroPacienteViewModel viewModel)
        {
            var res = await _usuarioService.RegistroPaciente(viewModel);
            if (res.Succeeded) return RedirectToAction("Login");
            return View("Registro");
        }

        public async Task<IActionResult> RegistrarPaciente(RegistroPacienteViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("RegistroPaciente", viewModel);
            var res = await _usuarioService.RegistroPaciente(viewModel);
            if (res.Succeeded) return RedirectToAction("Login");
            return View("Registro");
        }
        public async Task<IActionResult> RegistrarMedico(RegistroMedicoViewModel viewModel)
        {
            if (!ModelState.IsValid) return View("RegistroMedico", viewModel);
            var res = await _usuarioService.RegistroMedico(viewModel);
            if (res.Succeeded) return RedirectToAction("Login");
            return View("Registro");
        }

        public IActionResult RegistroPaciente()
        {
            return View();
        }

        public IActionResult RegistroMedico()
        {
            return View();
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
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Login", "Email ou Senha incorretos");
                return View("Login");
            }
        }
    }
}

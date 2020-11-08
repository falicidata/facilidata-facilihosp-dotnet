using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Facilidata.FaciliHosp.Presentation.Site.Models;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;

namespace Facilidata.FaciliHosp.Presentation.Site.Controllers
{

    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioAspNet _usuarioAspNet;
        private readonly IUsuarioService _usuarioService;
        private readonly IPlanoService _planoService;


        public HomeController(ILogger<HomeController> logger, IUsuarioAspNet usuarioAspNet, 
            IUsuarioService usuarioService, IPlanoService planoService)
        {
            _logger = logger;
            _usuarioAspNet = usuarioAspNet;
            _usuarioService = usuarioService;
            _planoService = planoService;
        }

        public IActionResult ComingSoon()
        {
            return View();
        }

        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
             
                return View("IndexUsuario");
            }
            else
            {
                return View();
            }

        }

        public IActionResult IndexUsuario()
        {
            string userName = _usuarioAspNet.GetUserName();
            ViewData["UserName"] = userName;
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Planos()
        {
            var planos = _planoService.ObterPlanos();
            var planoAtual = _usuarioService.ObterPlano();
            var planosView = new PlanoViewModel()
            {
                PlanoAtual = planoAtual,
                Planos = planos
            };
            return View("Planos", planosView);
        }

        public IActionResult AlterarPlano(string id)
        {
            _usuarioService.AlterarPlano(id);
            ViewData["Message"] = "Plano Alterado com Sucesso!";
            return View("IndexUsuario");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

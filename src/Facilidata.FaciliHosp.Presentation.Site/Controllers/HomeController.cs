﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Facilidata.FaciliHosp.Presentation.Site.Models;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;

namespace Facilidata.FaciliHosp.Presentation.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioAspNet _usuarioAspNet;
        private readonly IUsuarioService _usuarioService;


        public HomeController(ILogger<HomeController> logger, IUsuarioAspNet usuarioAspNet, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioAspNet = usuarioAspNet;
            _usuarioService = usuarioService;
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
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

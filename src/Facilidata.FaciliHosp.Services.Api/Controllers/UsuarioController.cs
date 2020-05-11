using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Facilidata.FaciliHosp.Services.Api.Configurations.JWT;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Services.Api.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly UserManager<Usuario> _userManager;
        public UsuarioController(IUnitOfWork<ContextSQL> uow, IUsuarioService usuarioService, TokenConfigurations tokenConfigurations, SigningConfigurations signingConfigurations, UserManager<Usuario> userManager) : base(uow)
        {
            _usuarioService = usuarioService;
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
            _userManager = userManager;
        }

        private void AdicionaErrosIdentityResultModelState(IdentityResult identityResult)
        {
            if (identityResult.Succeeded) return;
            identityResult.Errors
                 .ToList()
                 .ForEach(erro => AdicionarErroModelState(erro.Description, "IdentityResult"));
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("/api/token")]
        public async Task<IActionResult> Post([FromBody]LoginUsuarioViewModel viewModel)
        {
            var loginResultado = await _usuarioService.Login(viewModel);
            if (!loginResultado)
            {
                AdicionarErroModelState("Email ou senha incorretos");
                return Resposta();
            }

            var usuario = await _userManager.FindByEmailAsync(viewModel.Email);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Id));
            claims.Add(new Claim("UsuarioId", usuario.Id));

            GenericIdentity genericIdentity = new GenericIdentity(usuario.Email, "Login");
            ClaimsIdentity identity = new ClaimsIdentity(genericIdentity, claims);

            DateTime dtCreation = DateTime.Now;
            DateTime dtExpiration = dtCreation.AddSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dtCreation,
                Expires = dtExpiration
            };

            var securityToken = handler.CreateToken(securityTokenDescriptor);

            var token = handler.WriteToken(securityToken);

            return Resposta(new
            {
                email = usuario.Email,
                usuarioId = usuario.Id,
                created = dtCreation.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = dtExpiration.ToString("yyyy- MM-dd HH:mm:ss"),
                accessToken = token,
            });
        }


        [HttpPost("registro-medico")]
        public async Task<IActionResult> RegistroMedico([FromBody] RegistroViewModel viewModel)
        {
            if (!ModelState.IsValid) return Resposta();
            var resultadoRegistro = await _usuarioService.Registro(viewModel);
            AdicionaErrosIdentityResultModelState(resultadoRegistro);
            return Resposta();

        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _usuarioService.Logout();
            return Resposta();
        }

    }
}

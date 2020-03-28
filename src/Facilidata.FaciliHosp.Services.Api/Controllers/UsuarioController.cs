using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Services.Api.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUnitOfWork<ContextSQL> uow, IUsuarioService usuarioService) : base(uow)
        {
            _usuarioService = usuarioService;
        }

        private void AdicionaErrosIdentityResultModelState(IdentityResult identityResult)
        {
            if (identityResult.Succeeded) return;
            identityResult.Errors
                 .ToList()
                 .ForEach(erro => AdicionarErroModelState(erro.Description, "IdentityResult"));
        }


        //[HttpPost("registro")]
        //public async Task<IActionResult> Registro([FromBody] RegistroPacienteViewModel viewModel)
        //{
        //    if (!ModelState.IsValid) return Resposta();
        //    var resultadoRegistro = await _usuarioService.Registro(viewModel);
        //    AdicionaErrosIdentityResultModelState(resultadoRegistro);
        //    return Resposta();

        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginUsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid) return Resposta();
            var resultadoLogin = await _usuarioService.Login(viewModel);
            if (resultadoLogin == false) AdicionarErroModelState("Usuario ou Senha incorretos");
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

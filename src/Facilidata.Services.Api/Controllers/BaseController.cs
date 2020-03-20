using FaciliHosp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Facilidata.Services.Api.Controllers
{
    [Route("api/[controller]")]

    public abstract class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _uow;

        protected BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected IActionResult Resposta(object obj = null)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { Success = true, Data = obj });
            }
            else
            {
                var errosModelState = ErrosModelState();
                return BadRequest(new { Success = false, Data =  errosModelState }); ;
            }
        }

        protected void AddErroModelStage(string value ,string key = null)
        {
            this.ModelState.AddModelError(key, value);
        }

        protected object ErrosModelState()
        {
            if (ModelState.IsValid) return new object[] { };
            var errors = this.ModelState.Values.SelectMany(e => e.Errors);
            var errorsFormatado = errors.Select(e => new { Key = "ModelState", Value = e.ErrorMessage }).ToList();
            return errorsFormatado;
        }


        protected bool Commit()
        {
            try
            {
                return _uow.Commit();
            }
            catch (System.Exception e)
            {
                AddErroModelStage($"Ocorreu um erro ao realizar atualizações no banco de dados, Erro: {e.Message}", "Commit");
                return false;
            }
        }
    }
}

using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Facilidata.FaciliHosp.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private readonly IUnitOfWork<ContextSQL> _uow;

        protected BaseController(IUnitOfWork<ContextSQL> uow)
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
                var erros = ObterErrosModelState();
                return BadRequest(new { Success = false, Data = erros });
            }
        }

        protected void AdicionarErroModelState(string value, string key = null)
        {
            this.ModelState.AddModelError(key, value);
        }

        protected object ObterErrosModelState()
        {
            if (ModelState.IsValid) return new object[] { };
            var erros = ModelState.Values.SelectMany(e => e.Errors).ToList();
            var errosFormatados = erros.Select(erro => new { Key = "ModelState", Value = erro.ErrorMessage });
            return errosFormatados;
        }

        protected bool Commit()
        {
            try
            {
                bool res = _uow.Commit();
                if (!res) AdicionarErroModelState($"Ocorreu um erro ao atualizar o banco de dados, erro: 0 linhas alteras", "Commit");
                return _uow.Commit();
            }
            catch (Exception e)
            {
                AdicionarErroModelState($"Ocorreu um erro ao atualizar o banco de dados, erro: {e.Message}", "Commit");
                return false;
            }

        }
    }
}

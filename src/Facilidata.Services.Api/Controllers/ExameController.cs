using FaciliHosp.Domain.Entidades;
using FaciliHosp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Facilidata.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExameController : BaseController
    {
        private readonly IExameRepositorio _ExameRepositorio;

        public ExameController(IExameRepositorio ExameRepositorio, IUnitOfWork uow) : base(uow)
        {
            _ExameRepositorio = ExameRepositorio;

        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var Exameis = _ExameRepositorio.TrazerTodosJoinHospital();
            return Resposta(Exameis);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetPorId(Guid id)
        {
            var Hospital = _ExameRepositorio.TrazerPorId(id);
            return Resposta(Hospital);
        }

        [HttpPost]
        public IActionResult Post([FromBody]  Exame exame)
        {
            _ExameRepositorio.Inserir(exame);
            this.Commit();
            return Resposta();
        }

        [HttpPut]
        public IActionResult Put(Guid id, [FromBody] Exame exame)
        {
            exame.Id = id;
            _ExameRepositorio.Atualizar(id, exame);
            this.Commit();
            return Resposta();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            _ExameRepositorio.Deletar(id);
            return Resposta();
        }
    }
}

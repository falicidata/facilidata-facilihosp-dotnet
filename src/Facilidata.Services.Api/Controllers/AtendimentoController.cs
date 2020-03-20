using FaciliHosp.Domain.Entidades;
using FaciliHosp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Facilidata.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtendimentoController : BaseController
    {
        private readonly IAtendimentoRepositorio _AtendimentoRepositorio;

        public AtendimentoController(IAtendimentoRepositorio AtendimentoRepositorio, IUnitOfWork uow) : base(uow)
        {
            _AtendimentoRepositorio = AtendimentoRepositorio;

        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var Atendimentois = _AtendimentoRepositorio.TrazerTodos();
            return Resposta(Atendimentois);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetPorId(Guid id)
        {
            var Atendimento = _AtendimentoRepositorio.TrazerPorId(id);
            return Resposta(Atendimento);
        }

        [HttpPost]
        public IActionResult Post([FromBody]  Atendimento Atendimento)
        {
            _AtendimentoRepositorio.Inserir(Atendimento);
            this.Commit();
            return Resposta();
        }

        [HttpPut]
        public IActionResult Put(Guid id, [FromBody] Atendimento Atendimento)
        {
            Atendimento.Id = id;
            _AtendimentoRepositorio.Atualizar(id, Atendimento);
            this.Commit();
            return Resposta();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            _AtendimentoRepositorio.Deletar(id);
            return Resposta();
        }
    }
}

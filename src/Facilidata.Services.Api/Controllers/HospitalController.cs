using FaciliHosp.Domain.Entidades;
using FaciliHosp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Facilidata.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HospitalController : BaseController
    {
        private readonly IHospitalRepositorio _hospitalRepositorio;

        public HospitalController(IHospitalRepositorio hospitalRepositorio, IUnitOfWork uow) : base(uow)
        {
            _hospitalRepositorio = hospitalRepositorio;

        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var hospitais = _hospitalRepositorio.TrazerTodos();
            return Resposta(hospitais);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetPorId(Guid id)
        {
            var hospital = _hospitalRepositorio.TrazerPorId(id);
            return Resposta(hospital);
        }

        [HttpPost]
        public IActionResult Post([FromBody]  Hospital hospital)
        {
            _hospitalRepositorio.Inserir(hospital);
            this.Commit();
            return Resposta();
        }

        [HttpPut]
        public IActionResult Put(Guid id, [FromBody] Hospital hospital)
        {
            hospital.Id = id;
            _hospitalRepositorio.Atualizar(id, hospital);
            this.Commit();
            return Resposta();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            _hospitalRepositorio.Deletar(id);
            return Resposta();
        }
    }
}

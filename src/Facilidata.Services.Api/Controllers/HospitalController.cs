using FaciliHosp.Domain.Entidades;
using FaciliHosp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Facilidata.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HospitalController : ControllerBase
    {
        private readonly IHospitalRepositorio _hospitalRepositorio;
        private readonly IUnitOfWork _uow;

        public HospitalController(IHospitalRepositorio hospitalRepositorio, IUnitOfWork uow)
        {
            _hospitalRepositorio = hospitalRepositorio;
            _uow = uow;
        }


        [HttpGet]
        public IActionResult GetTodos()
        {
            var hospitais = _hospitalRepositorio.TrazerTodos();
            return Ok(hospitais);
        }

        // api/hospital/idakslnflsdnfndlsf
        [HttpGet("{id:Guid}")]
        public IActionResult GetPorId(Guid id)
        {
            var hospital = _hospitalRepositorio.TrazerPorId(id);
            return Ok(hospital);
        }

        [HttpPost]
        public IActionResult Post([FromBody]  Hospital hospital)
        {
            _hospitalRepositorio.Inserir(hospital);
            var res = _uow.Commit();
            
            if (res == true)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        public IActionResult Put(Guid id, [FromBody] Hospital hospital)
        {
            _hospitalRepositorio.Atualizar(id,hospital);
            var res = _uow.Commit();

            if (res == true)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            _hospitalRepositorio.Deletar(id);
            var res = _uow.Commit();

            if (res == true)
                return Ok();
            else
                return BadRequest();
        }
    }
}

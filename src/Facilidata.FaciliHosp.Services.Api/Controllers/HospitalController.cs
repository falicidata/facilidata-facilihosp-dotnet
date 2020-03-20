using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Facilidata.FaciliHosp.Services.Api.Controllers
{
    public class HospitalController : BaseController
    {
        private readonly IHospitalRepository _hospitalRepository;

        public HospitalController(IUnitOfWork uow, IHospitalRepository hospitalRepository) : base(uow)
        {
            _hospitalRepository = hospitalRepository;
        }

        [HttpGet]
        public IActionResult GetObterTodos()
        {
            var hospitais = _hospitalRepository.ObterTodos();
            return Resposta(hospitais);
        }

        [HttpGet("{id}")]
        public IActionResult GetObterPorId(string id)
        {
            var hospital = _hospitalRepository.ObterPorId(id);
            return Resposta(hospital);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Hospital hospital)
        {
            _hospitalRepository.Inserir(hospital);
            Commit();
            return Resposta();
        }

        [HttpPut]
        public IActionResult Put(string id, [FromBody] Hospital hospital)
        {
            _hospitalRepository.Atualizar(id, hospital);
            Commit();
            return Resposta();
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            _hospitalRepository.Deletar(id);
            Commit();
            return Resposta();

        }
    }
}

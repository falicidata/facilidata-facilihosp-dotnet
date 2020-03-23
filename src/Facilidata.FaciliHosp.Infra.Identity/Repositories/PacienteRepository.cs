using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Repositories;

namespace Facilidata.FaciliHosp.Infra.Identity.Repositories
{
    public class PacienteRepository : Repository<Paciente, ContextIdentity>, IPacienteRepository
    {
        public PacienteRepository(ContextIdentity context) : base(context)
        {
        }
    }
}

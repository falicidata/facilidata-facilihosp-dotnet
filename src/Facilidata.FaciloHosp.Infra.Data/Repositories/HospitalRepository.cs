using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;

namespace Facilidata.FaciloHosp.Infra.Data.Repositories
{
    public class HospitalRepository : Repository<Hospital, ContextSQLS>, IHospitalRepository
    {
        public HospitalRepository(ContextSQLS context) : base(context)
        {
            
        }
    }
}

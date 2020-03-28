using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;

namespace Facilidata.FaciloHosp.Infra.Data.Repositories
{
    public class HospitalRepository : Repository<Hospital, ContextSQL>, IHospitalRepository
    {
        public HospitalRepository(ContextSQL context) : base(context)
        {
            
        }
    }
}

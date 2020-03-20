using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using System.Collections.Generic;

namespace Facilidata.FaciloHosp.Infra.Data.Repositories
{
    public class HospitalRepository : Repository<Hospital>, IHospitalRepository
    {
        public HospitalRepository(ContextSQLS context) : base(context)
        {
            
        }
    }
}

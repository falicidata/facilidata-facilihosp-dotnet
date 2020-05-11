using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;

namespace Facilidata.FaciloHosp.Infra.Data.Repositories
{
    public class ExameTipoRepository : Repository<ExameTipo, ContextSQL>, IExameTipoRepository
    {
        public ExameTipoRepository(ContextSQL context) : base(context)
        {
        }
    }
}

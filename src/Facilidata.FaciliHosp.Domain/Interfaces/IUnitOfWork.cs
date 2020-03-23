using Microsoft.EntityFrameworkCore;

namespace Facilidata.FaciliHosp.Domain.Interfaces
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        bool Commit();
    }
}

using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using System;

namespace Facilidata.FaciloHosp.Infra.Data.UnitOfWork
{
    public class UnitOfWorkSQLS : IUnitOfWork<ContextSQLS>
    {
        private readonly ContextSQLS _context;
        public UnitOfWorkSQLS(ContextSQLS context)
        {
            _context = context;
        }
        public bool Commit()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

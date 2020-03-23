using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Context;
using System;

namespace Facilidata.FaciliHosp.Infra.Identity.UnitOfWork
{
    public class UnitOfWorkIdentity : IUnitOfWork<ContextIdentity>
    {
        private readonly ContextIdentity _context;
        public UnitOfWorkIdentity(ContextIdentity context)
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

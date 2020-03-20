using FaciliHosp.Domain.Interfaces;
using FaciliHosp.Infra.Data.Context;
using System;

namespace FaciliHosp.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextSQLServer _context;
        public UnitOfWork(ContextSQLServer context)
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

                throw;
            }
        }
    }
}

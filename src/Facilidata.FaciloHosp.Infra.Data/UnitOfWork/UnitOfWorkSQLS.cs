﻿using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using System;

namespace Facilidata.FaciloHosp.Infra.Data.UnitOfWork
{
    public class UnitOfWorkSQLS : IUnitOfWork<ContextSQL>
    {
        private readonly ContextSQL _context;
        public UnitOfWorkSQLS(ContextSQL context)
        {
            _context = context;
        }
        public bool Commit()
        {
            try
            {
                int resultado = _context.SaveChanges();
                return resultado > 0;
            }
            catch (Exception e)
            {
                string erroMessage = e.Message;
                return false;
            }
        }
    }
}

using FaciliHosp.Domain.Entidades;
using FaciliHosp.Domain.Interfaces;
using FaciliHosp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FaciliHosp.Infra.Data.Repositorios
{
    public class ExameRepositorio : Repositorio<Exame>, IExameRepositorio
    {
        public ExameRepositorio(ContextSQLServer context) : base(context)
        {
        }


        public List<Exame> TrazerTodosJoinHospital()
        {
            return this._dbset.Include(e => e.Hospital)
                                    .Where(e => e.Deletado == false)
                                    .ToList();

        }
    }
}

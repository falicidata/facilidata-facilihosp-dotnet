using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Facilidata.FaciloHosp.Infra.Data.Repositories
{
    public class ExameRepository : Repository<Exame,ContextSQLS>, IExameRepository
    {
        public ExameRepository(ContextSQLS context) : base(context)
        {
        }

        public List<Exame> ObterTodosJoinHospital()
        {
            return _dbSet.Where(exame => exame.Deletado == false).Include(exame => exame.Hospital).ToList();
        }

        public List<Exame> ObterTodosPorHospitalIdPorUsuarioId(string hospitalId, string usuarioId)
        {
            return _dbSet.Where(exame => !exame.Deletado && exame.HospitalId == hospitalId && exame.UserId == usuarioId).ToList();
        }

    }
}

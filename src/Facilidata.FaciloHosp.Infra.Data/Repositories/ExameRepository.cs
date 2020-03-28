using Dapper;
using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Domain.Models;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Facilidata.FaciloHosp.Infra.Data.Repositories
{
    public class ExameRepository : Repository<Exame,ContextSQL>, IExameRepository
    {
        public ExameRepository(ContextSQL context) : base(context)
        {
        }

        public List<ExameSemAnexo> ObterTodosSemAnexoPorHospitalIdEUsuarioId(string hospitalId, string usuarioId)
        {
            using (var conexão = _context.Database.GetDbConnection())
            {
                string query = "select id, tipo, hospitalId,usuarioId, criadoEm from exames where deletado = 0 and hospitalId = @hospitalId and usuarioId = @usuarioId";
                var resultado = conexão.Query<ExameSemAnexo>(query, new { UsuarioId = usuarioId, HospitalId = hospitalId }).ToList();
                return resultado;
            }
        }

        public List<Exame> ObterTodosJoinHospital()
        {
            return _dbSet.Where(exame => exame.Deletado == false).Include(exame => exame.Hospital).ToList();
        }

        public List<Exame> ObterTodosPorHospitalIdPorUsuarioId(string hospitalId, string usuarioId)
        {
            return _dbSet.Where(exame => !exame.Deletado && exame.HospitalId == hospitalId && exame.UsuarioId == usuarioId).ToList();
        }

    }
}

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
                string query = $"select \"Id\", \"Tipo\", \"HospitalId\",\"UsuarioId\", \"CriadoEm\" from \"Exames\" where \"Deletado\" = 0  and \"HospitalId\" = '{hospitalId}' and \"UsuarioId\" = '{usuarioId}'";
                var resultado = conexão.Query<ExameSemAnexo>(query).ToList();
                return resultado;
            }
        }

        public List<ExameComHospitaisUsuarios> ObterTodosSemAnexoComHospitalEUsuario()
        {
            using (var conexão = _context.Database.GetDbConnection())
            {
                string query = $"select e.\"Id\",e.\"HospitalId\",e.\"UsuarioId\",u.\"Email\", e.\"Tipo\",e.\"CriadoEm\",h.\"Nome\" Hospital from \"Exames\"  e join \"Hospitais\" h on h.\"Id\" = e.\"HospitalId\" join \"AspNetUsers\" u on u.\"Id\" = e.\"UsuarioId\" where e.\"Deletado\" = 0";
                var resultado = conexão.Query<ExameComHospitaisUsuarios>(query).ToList();
                return resultado;
            }
        }

        public List<ExameComHospitaisUsuarios> ObterTodosSemAnexoComHospitalEUsuarioPorUsuarioId(string usuarioId)
        {
            using (var conexão = _context.Database.GetDbConnection())
            {
                string query = $"select e.\"Id\",e.\"HospitalId\",e.\"UsuarioId\",u.\"Email\", e.\"Tipo\",e.\"CriadoEm\",h.\"Nome\" Hospital from \"Exames\"  e join \"Hospitais\" h on h.\"Id\" = e.\"HospitalId\" join \"AspNetUsers\" u on u.\"Id\" = e.\"UsuarioId\" where e.\"Deletado\" = 0 and e.\"UsuarioId\" = {usuarioId}";
                var resultado = conexão.Query<ExameComHospitaisUsuarios>(query).ToList();
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

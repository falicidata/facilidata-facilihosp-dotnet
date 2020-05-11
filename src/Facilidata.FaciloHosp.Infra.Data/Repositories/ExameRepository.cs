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

        public List<ExameSemAnexo> ObterTodosSemAnexoPorUsuarioId(string usuarioId)
        {
            using (var conexão = _context.Database.GetDbConnection())
            {
                string query = $"select Id, TipoOutro,Url, Fornecedor, Formato, TipoId,UsuarioId, CriadoEm from Exames where Deletado = 0 and UsuarioId = '{usuarioId}'";
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
                string query = $"select e.\"Id\",e.\"HospitalId\",e.\"UsuarioId\",e.\"CriadoPor\",u.\"Email\", e.\"Tipo\",e.\"CriadoEm\",h.\"Nome\" Hospital from \"Exames\"  e join \"Hospitais\" h on h.\"Id\" = e.\"HospitalId\" join \"AspNetUsers\" u on u.\"Id\" = e.\"UsuarioId\" where e.\"Deletado\" = 0 and e.\"UsuarioId\" = '{usuarioId}'";
                var resultado = conexão.Query<ExameComHospitaisUsuarios>(query).ToList();
                return resultado;
            }
        }


 

    }
}

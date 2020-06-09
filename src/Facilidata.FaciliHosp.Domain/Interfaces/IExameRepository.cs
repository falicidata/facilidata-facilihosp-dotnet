using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Models;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Domain.Interfaces
{
    public interface IExameRepository : IRepository<Exame>
    {
        List<ExameComHospitaisUsuarios> ObterTodosSemAnexoComHospitalEUsuario();
        List<ExameComHospitaisUsuarios> ObterTodosSemAnexoComHospitalEUsuarioPorUsuarioId(string usuarioId);
        List<ExameSemAnexo> ObterTodosSemAnexoPorUsuarioId(string usuarioId);
        List<TipoExame> ObterExamesTipos();

    }
}

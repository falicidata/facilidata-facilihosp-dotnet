using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Models;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Domain.Interfaces
{
    public interface IExameRepository : IRepository<Exame>
    {
        List<Exame> ObterTodosJoinHospital();
        List<ExameComHospitaisUsuarios> ObterTodosSemAnexoComHospitalEUsuario();
        List<ExameComHospitaisUsuarios> ObterTodosSemAnexoComHospitalEUsuarioPorUsuarioId(string usuarioId);
        List<Exame> ObterTodosPorHospitalIdPorUsuarioId(string hospitalId, string usuarioId);
        List<ExameSemAnexo> ObterTodosSemAnexoPorHospitalIdEUsuarioId(string hospitalId, string usuarioId);

    }
}

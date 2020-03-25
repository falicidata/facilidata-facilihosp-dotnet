using Facilidata.FaciliHosp.Domain.Entidades;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Domain.Interfaces
{
    public interface IExameRepository : IRepository<Exame>
    {
        List<Exame> ObterTodosJoinHospital();
        List<Exame> ObterTodosPorHospitalIdPorUsuarioId(string hospitalId, string usuarioId);

    }
}

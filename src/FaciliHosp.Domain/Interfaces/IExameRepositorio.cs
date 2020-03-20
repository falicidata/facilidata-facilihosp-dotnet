using FaciliHosp.Domain.Entidades;
using System.Collections.Generic;

namespace FaciliHosp.Domain.Interfaces
{
    public interface IExameRepositorio : IRepositorio<Exame>
    {
        List<Exame> TrazerTodosJoinHospital();
    }
}

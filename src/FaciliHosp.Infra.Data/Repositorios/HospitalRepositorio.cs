using FaciliHosp.Domain.Entidades;
using FaciliHosp.Domain.Interfaces;
using FaciliHosp.Infra.Data.Context;

namespace FaciliHosp.Infra.Data.Repositorios
{
    public class HospitalRepositorio : Repositorio<Hospital>, IHospitalRepositorio
    {
        public HospitalRepositorio(ContextSQLServer context) : base(context)
        {
        }


    }
}

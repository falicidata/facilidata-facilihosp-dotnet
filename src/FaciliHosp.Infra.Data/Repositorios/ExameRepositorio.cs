using FaciliHosp.Domain.Entidades;
using FaciliHosp.Domain.Interfaces;
using FaciliHosp.Infra.Data.Context;

namespace FaciliHosp.Infra.Data.Repositorios
{
    public class ExameRepositorio : Repositorio<Exame>, IExameRepositorio
    {
        public ExameRepositorio(ContextSQLServer context) : base(context)
        {
        }


    }
}

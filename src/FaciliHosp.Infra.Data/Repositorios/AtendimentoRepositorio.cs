using FaciliHosp.Domain.Entidades;
using FaciliHosp.Domain.Interfaces;
using FaciliHosp.Infra.Data.Context;

namespace FaciliHosp.Infra.Data.Repositorios
{
    public class AtendimentoRepositorio : Repositorio<Atendimento>, IAtendimentoRepositorio
    {
        public AtendimentoRepositorio(ContextSQLServer context) : base(context)
        {
        }
    }
}

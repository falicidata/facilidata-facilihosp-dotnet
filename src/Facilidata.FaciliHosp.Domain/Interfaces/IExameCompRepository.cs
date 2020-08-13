using Facilidata.FaciliHosp.Domain.Entidades;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Domain.Interfaces
{
    public interface IExameCompRepository : IRepository<ExameComp>
    {
        ExameComp ObterPorKey(string key);
        void AdicionarUsuario(string key, string usuarioId);
        List<ExameComp> ObterTodosPorUsuarioLogado();
        List<ExameComp> ObterTodosPorUsuarioCompartilhado();
    }
}

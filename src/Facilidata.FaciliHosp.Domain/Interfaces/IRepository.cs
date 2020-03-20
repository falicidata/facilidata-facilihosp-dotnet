using Facilidata.FaciliHosp.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Facilidata.FaciliHosp.Domain.Interfaces
{
    public interface IRepository<T> where T : Entidade
    {
        List<T> ObterTodos();
        T ObterPorId(string id);
        void Inserir(T entidade);
        void Atualizar(string id, T entidade);
        void Deletar(string id);
        List<T> Pesquisar(Expression<Func<T, bool>> expression);
    }
}

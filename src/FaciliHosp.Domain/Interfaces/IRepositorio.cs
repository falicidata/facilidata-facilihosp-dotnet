using FaciliHosp.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FaciliHosp.Domain.Interfaces
{
    public interface IRepositorio<T> where T : Entidade
    {
        List<T> TrazerTodos();
        T TrazerPorId(Guid id);
        void Inserir(T entidade);
        void Atualizar(Guid id,T entidade);
        void Deletar(Guid id);
        List<T> Pesquisar(Expression<Func<T, bool>> expression);
    }
}

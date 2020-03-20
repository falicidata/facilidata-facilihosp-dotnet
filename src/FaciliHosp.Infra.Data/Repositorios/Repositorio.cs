using FaciliHosp.Domain.Entidades;
using FaciliHosp.Domain.Interfaces;
using FaciliHosp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FaciliHosp.Infra.Data.Repositorios
{
    public abstract class Repositorio<T> : IRepositorio<T> where T : Entidade
    {
        protected readonly ContextSQLServer _context;
        protected readonly DbSet<T> _dbset;

        protected Repositorio(ContextSQLServer context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public virtual void Atualizar(Guid id, T entidade)
        {
            var entidadeFind = _dbset.AsNoTracking().FirstOrDefault(e => e.Id == id);
            var entidadeExist = entidadeFind != null;
            if (!entidadeExist) return;
            _dbset.Update(entidade);
        }

        public virtual void Deletar(Guid id)
        {
            var entidade = _dbset.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if (entidade == null) return;
            entidade.Deletado = true;
            _dbset.Update(entidade);
        }

        public virtual void Inserir(T entidade)
        {
            _dbset.Add(entidade);
        }

        public virtual List<T> Pesquisar(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(e => e.Deletado == false).Where(expression).ToList();
        }

        public virtual T TrazerPorId(Guid id)
        {
            return _dbset.Find(id);
        }

        public virtual List<T> TrazerTodos()
        {
            return _dbset.Where(e => e.Deletado == false).ToList();
        }
    }
}

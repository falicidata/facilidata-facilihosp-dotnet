using FaciliHosp.Domain.Entidades;
using FaciliHosp.Domain.Interfaces;
using FaciliHosp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FaciliHosp.Infra.Data.Repositorios
{
    public abstract class Repositorio<T> : IRepositorio<T> where T : Entidade
    {
        private readonly ContextSQLServer _context;
        private readonly DbSet<T> _dbset;

        protected Repositorio(ContextSQLServer context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public void Atualizar(Guid id, T entidade)
        {
            var entidadeFind = _dbset.Find(id);
            if (entidadeFind == null) return;

            _context.Attach(entidade);
           _context.Entry(entidadeFind).State = EntityState.Modified;

        }

        public void Deletar(Guid id)
        {
            var entidade = _dbset.Find(id);
            if (entidade == null) return;
            entidade.Deletado = true;
            _dbset.Update(entidade);
        }

        public void Inserir(T entidade)
        {
            _dbset.Add(entidade);
        }

        public List<T> Pesquisar(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(e => e.Deletado == false).Where(expression).ToList();
        }

        public T TrazerPorId(Guid id)
        {
            return _dbset.Find(id);
        }

        public List<T> TrazerTodos()
        {
            return _dbset.Where(e => e.Deletado == false).ToList();
        }
    }
}

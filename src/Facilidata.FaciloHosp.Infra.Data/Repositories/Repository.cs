using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Facilidata.FaciloHosp.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entidade
    {
        protected readonly ContextSQLS _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ContextSQLS context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual void Deletar(string id)
        {
            var entidadeFind = _dbSet.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if (entidadeFind == null) return;
            entidadeFind.Deletado = true;
            this.Atualizar(id,entidadeFind);
           
        }

        public virtual void Inserir(T entidade)
        {
            _dbSet.Add(entidade);
        }

        public virtual T ObterPorId(string id)
        {
            return _dbSet.Find(id);
        }

        public virtual List<T> ObterTodos()
        {
            return _dbSet.Where(e => e.Deletado == false).ToList();
        }

        public virtual List<T> Pesquisar(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(e => !e.Deletado).Where(expression).ToList();
        }

        public virtual void Atualizar(string id, T entidade)
        {
            var entidadeFind = _dbSet.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if (entidadeFind == null) return;
            entidade.Id = id;
            _dbSet.Update(entidade);
        }
    }
}

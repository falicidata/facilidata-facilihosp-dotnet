using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Facilidata.FaciloHosp.Infra.Data.Repositories
{
    public class Repository<TEntidade, TContext> : IRepository<TEntidade> where TEntidade : Entidade where TContext : DbContext
    {
        protected readonly TContext _context;
        protected readonly DbSet<TEntidade> _dbSet;

        public Repository(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntidade>();
        }
        public virtual void Deletar(string id)
        {
            var entidadeFind = _dbSet.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if (entidadeFind == null) return;
            entidadeFind.Deletado = true;
            this.Atualizar(id, entidadeFind);

        }

        public virtual void Inserir(TEntidade entidade)
        {
            _dbSet.Add(entidade);
        }

        public virtual TEntidade ObterPorId(string id)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        public virtual List<TEntidade> ObterTodos()
        {
            return _dbSet.Where(e => e.Deletado == false).ToList();
        }

        public virtual List<TEntidade> Pesquisar(Expression<Func<TEntidade, bool>> expression)
        {
            return _dbSet.Where(e => !e.Deletado).Where(expression).ToList();
        }

        public virtual void Atualizar(string id, TEntidade entidade)
        {
            var entidadeFind = _dbSet.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if (entidadeFind == null) return;
            _dbSet.Remove(entidadeFind);
        }
    }
}

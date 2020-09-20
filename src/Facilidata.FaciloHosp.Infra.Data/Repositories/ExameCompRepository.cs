using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Facilidata.FaciloHosp.Infra.Data.Repositories
{
    public class ExameCompRepository : Repository<ExameComp, ContextSQL>, IExameCompRepository
    {
        private readonly IUsuarioAspNet _usuarioAspNet;
        public ExameCompRepository(ContextSQL context, IUsuarioAspNet usuarioAspNet) : base(context)
        {
            _usuarioAspNet = usuarioAspNet;
        }

        public List<ExameComp> ObterTodosPorUsuarioCompartilhado()
        {
            var comp = _dbSet.Include(e => e.Exame)
                        .Where(ec => ec.UsuarioId == _usuarioAspNet.GetUsuarioId() && ec.Exame != null)
                        .ToList();

            return comp.Where(e => e.ExpiraEm >= DateTime.Now).ToList();
        }

        public List<ExameComp> ObterTodosPorUsuarioLogado()
        {
            string usuarioId = _usuarioAspNet.GetUsuarioId();
            var comp = _dbSet.Include(e => e.Exame)
                        .Where(ec => ec.Exame.UsuarioId == usuarioId  && ec.Exame != null).ToList();

            return comp.Where(ec => ec.ExpiraEm >= DateTime.Now).ToList();
        }

        public ExameComp ObterPorKey(string key)
        {
            return this._dbSet.FirstOrDefault(ec => ec.Key == key);
        }

        public void AdicionarUsuario(string key, string usuarioId)
        {
            var exameComp = ObterPorKey(key);
            exameComp.UsuarioId = usuarioId;
            Atualizar(exameComp.Id, exameComp);
        }
    }
}

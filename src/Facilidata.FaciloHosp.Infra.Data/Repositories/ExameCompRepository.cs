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
            string usuarioId = _usuarioAspNet.GetUsuarioId();
            return this._dbSet.Include(e => e.Exame)
                        .Where(ec => ec.UsuarioId == usuarioId && ec.ExpiraEm >= DateTime.Now && ec.Exame != null)
                        .ToList();
        }

        public List<ExameComp> ObterTodosPorUsuarioLogado()
        {
            string usuarioId = _usuarioAspNet.GetUsuarioId();
            return this._dbSet.Include(e => e.Exame)
                        .Where(ec => ec.Exame.UsuarioId == usuarioId && ec.ExpiraEm >= DateTime.Now && ec.Exame != null).ToList();
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

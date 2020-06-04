using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace Facilidata.FaciloHosp.Infra.Data.Repositories
{
    public class ExameTipoRepository : Repository<ExameTipo, ContextSQL>, IExameTipoRepository
    {
        public ExameTipoRepository(ContextSQL context) : base(context)
        {
        }


        public ExameTipo InsereSeNaoExistir(string tipo)
        {
            if (string.IsNullOrEmpty(tipo)) return null;
            string nome = tipo.ToLower().Trim();
            ExameTipo exameTipoBanco = _dbSet.FirstOrDefault(exameTipo => exameTipo.Nome.ToLower().Trim() == nome);
            if (exameTipoBanco != null) return exameTipoBanco;
            ExameTipo novoExame = new ExameTipo() { Nome = tipo };
            _dbSet.Add(novoExame);
            _context.SaveChanges();
            return novoExame;
        }
    }
}

using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Domain.Entidades
{
    public class ExameTipo : Entidade
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }

        // Entity Framework
        public List<Exame> Exames { get; set; }
    }
}

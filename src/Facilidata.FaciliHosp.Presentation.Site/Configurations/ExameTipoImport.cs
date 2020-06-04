using Facilidata.FaciliHosp.Domain.Entidades;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Presentation.Site.Configurations
{
    public class ExameTipoImport
    {
        public ExameTipoImport()
        {
            Tipos = new List<string>();
        }
        public List<string> Tipos { get; set; }
    }
}

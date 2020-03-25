using Facilidata.FaciliHosp.Domain.Entidades;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Application.ViewModels
{
    public class ExameIndexViewModel
    {
        public string UsuarioId { get; set; }
        public string HospitalId { get; set; }
        public List<Exame> Exames { get; set; }
    }
}

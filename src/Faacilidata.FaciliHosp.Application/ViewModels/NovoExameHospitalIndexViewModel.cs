using Facilidata.FaciliHosp.Domain.Entidades;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Application.ViewModels
{
    public class NovoExameHospitalIndexViewModel
    {
        public string UsuarioId { get; set; }
        public List<Hospital> Hospitais { get; set; }
    }
}

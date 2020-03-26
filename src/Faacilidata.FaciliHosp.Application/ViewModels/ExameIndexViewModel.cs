using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Models;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Application.ViewModels
{
    public class ExameIndexViewModel
    {
        public string UsuarioId { get; set; }
        public string HospitalId { get; set; }
        public List<ExameSemAnexo> Exames { get; set; }
    }
}

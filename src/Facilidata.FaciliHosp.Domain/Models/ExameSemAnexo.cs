using System;

namespace Facilidata.FaciliHosp.Domain.Models
{
    public class ExameSemAnexo
    {
        public string Id { get; set; }
        public string Tipo { get; set; }
        public string HospitalId { get; set; }
        public string UsuarioId { get; set; }
        public DateTime? CriadoEm { get; set; }
    }
}

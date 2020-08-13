using System;

namespace Facilidata.FaciliHosp.Domain.Models
{
    public class ExameSemAnexo
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string TipoOutro { get; set; }
        public string Formato { get; set; }
        public string Fornecedor { get; set; }
        public string UsuarioId { get; set; }
        public string RetornoUsuario { get; set; }
        public string Retorno{ get; set; }
        public DateTime? CriadoEm { get; set; }
    }
}

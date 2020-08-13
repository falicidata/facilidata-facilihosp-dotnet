using Facilidata.FaciliHosp.Domain.Enums;

namespace Facilidata.FaciliHosp.Application.ViewModels
{
    public class RetornoViewModel
    {
        public string ExameId { get; set; }
        public EExameResultadoAvaliacao ResultadoAvaliacao { get; set; }
        public string Retorno{ get; set; }
        public string RetornoUsuario { get; set; }
    }
}

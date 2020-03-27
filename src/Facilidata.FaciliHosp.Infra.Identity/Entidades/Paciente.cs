using Facilidata.FaciliHosp.Domain.Entidades;

namespace Facilidata.FaciliHosp.Infra.Identity.Entidades
{
    public class Paciente : Conta
    {
        public string CPF { get; set; }
        public string ConvenioMedico { get; set; }
    }
}

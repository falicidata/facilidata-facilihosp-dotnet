using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Enums;
using Facilidata.FaciliHosp.Infra.Identity.Models;

namespace Facilidata.FaciliHosp.Infra.Identity.Entidades
{
    public abstract class Conta : Entidade
    {

        public ESexoConta Sexo { get; set; }
        public Usuario Usuario { get; set; }
    }
}

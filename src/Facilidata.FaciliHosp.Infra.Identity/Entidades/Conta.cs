using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Models;

namespace Facilidata.FaciliHosp.Infra.Identity.Entidades
{
    public abstract class Conta : Entidade
    {
        public Usuario Usuario { get; set; }
    }
}

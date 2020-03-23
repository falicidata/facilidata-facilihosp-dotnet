using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Microsoft.AspNetCore.Identity;

namespace Facilidata.FaciliHosp.Infra.Identity.Models
{
    public class Usuario : IdentityUser
    {
        public string ContaId { get; set; }
        public Conta Conta { get; set; }
    }
}

using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Microsoft.AspNetCore.Identity;

namespace Facilidata.FaciliHosp.Infra.Identity.Models
{
    public class Usuario : IdentityUser
    {
        protected Usuario()
        {

        }

        public Usuario(string email, string contaId)
        {
            Email = email;
            UserName = email;
            ContaId = contaId;
        }

        public string ContaId { get; set; }
        public Conta Conta { get; set; }
    }
}

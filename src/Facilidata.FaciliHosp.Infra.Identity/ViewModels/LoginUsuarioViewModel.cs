using Facilidata.FaciliHosp.Infra.Identity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Facilidata.FaciliHosp.Infra.Identity.ViewModels
{
    public  class LoginUsuarioViewModel
    {
        [Required(ErrorMessage = "Email Obrigatório"), EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha Obrigatória")]
        public string Senha { get; set; }
   
        public ETipoUsuario? Tipo { get; set; }
    }
}

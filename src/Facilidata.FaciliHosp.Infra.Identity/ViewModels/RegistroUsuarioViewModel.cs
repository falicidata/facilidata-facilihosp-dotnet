using Facilidata.FaciliHosp.Infra.Identity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Facilidata.FaciliHosp.Infra.Identity.ViewModels
{
    public class RegistroUsuarioViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required, Compare("Senha")]
        public string ConfirmacaoSenha { get; set; }
        [Required]
        public ETipoUsuario? Tipo { get; set; }
    }
}

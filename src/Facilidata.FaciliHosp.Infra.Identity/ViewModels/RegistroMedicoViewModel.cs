using Facilidata.FaciliHosp.Infra.Identity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Facilidata.FaciliHosp.Infra.Identity.ViewModels
{
    public class RegistroMedicoViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required, Compare("Senha")]
        public string ConfirmacaoSenha { get; set; }
        [Required]
        public string CRM { get; set; }
        [Required]
        public ESexoConta? Sexo { get; set; }
    }
}

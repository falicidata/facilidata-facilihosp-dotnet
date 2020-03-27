using Facilidata.FaciliHosp.Infra.Identity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Facilidata.FaciliHosp.Infra.Identity.ViewModels
{
    public class RegistroPacienteViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required, Compare("Senha")]
        public string ConfirmacaoSenha { get; set; }
        [Required]
        public string CPF { get; set; }
        public string ConvenioMedico { get; set; }
        [Required]
        public string Sexo { get; set; }

    }
}

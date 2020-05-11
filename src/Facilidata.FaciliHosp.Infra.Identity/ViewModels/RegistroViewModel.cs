using System;
using System.ComponentModel.DataAnnotations;

namespace Facilidata.FaciliHosp.Infra.Identity.ViewModels
{
    public class RegistroViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required, Compare("Senha", ErrorMessage = "As senhas precisam ser iguais")]
        public string ConfirmacaoSenha { get; set; }
        [Required]
        public DateTime? DataNascimento { get; set; }
        public string CRM { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Sexo { get; set; }
    }
}

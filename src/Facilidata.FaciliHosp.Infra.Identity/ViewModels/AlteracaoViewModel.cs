using System;
using System.ComponentModel.DataAnnotations;

namespace Facilidata.FaciliHosp.Infra.Identity.ViewModels
{
    public class AlteracaoViewModel
    {
        public string Id { get; set; }
        public string IdUsuario { get; set; }
        [Required(ErrorMessage = "Nome Obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Email Obrigatório"), EmailAddress]
        public string Email { get; set; }
        public string SenhaAntiga { get; set; }
        public string Senha { get; set; }
        [Compare("Senha", ErrorMessage = "As senhas precisam ser iguais")]
        public string ConfirmacaoSenha { get; set; }
        [Required(ErrorMessage = "Data de Nascimento Obrigatória")]
        public DateTime? DataNascimento { get; set; }
        [Required(ErrorMessage = "CPF Obrigatório")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Sexo Obrigatório")]
        public string Sexo { get; set; }
    }
}

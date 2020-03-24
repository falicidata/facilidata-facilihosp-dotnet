using System.ComponentModel.DataAnnotations;

namespace Facilidata.FaciliHosp.Application.ViewModels
{
    public class EditarHospitalViewModel
    {
        public string Id { get; set; }
        
        [Required]
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}

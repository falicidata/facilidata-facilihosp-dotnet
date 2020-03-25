using System.ComponentModel.DataAnnotations;

namespace Facilidata.FaciliHosp.Application.ViewModels
{
    public class EditarExameViewModel
    {
        public string Id { get; set; }
        public string HospitalId { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Tipo { get; set; }
        public string Resultado { get; set; }
        public string Url { get; set; }
        public byte[] Anexo { get; set; }

    }
}

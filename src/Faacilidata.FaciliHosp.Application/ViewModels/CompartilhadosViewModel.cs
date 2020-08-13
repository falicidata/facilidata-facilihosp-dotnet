using System;

namespace Facilidata.FaciliHosp.Application.ViewModels
{
    public class CompartilhadosViewModel
    {
        public string Id { get; set; }
        public string ExameId { get; set; }
        public DateTime Data { get; set; }
        public DateTime ExpiraEm { get; set; }
        public string Tipo { get; set; }
        public string UsuarioCompartilhado { get; set; }
    }
}

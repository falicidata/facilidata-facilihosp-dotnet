using Facilidata.FaciliHosp.Domain.Enums;

namespace Facilidata.FaciliHosp.Application.ViewModels
{
    public class ExameCompViewModel
    {
        public string  ExameId { get; set; }
        public string Key { get; set; }
        public EPeriodoComp Periodo { get; set; }
        public string Url { get; set; }
    }
}

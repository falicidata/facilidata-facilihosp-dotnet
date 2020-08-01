namespace Facilidata.FaciliHosp.Presentation.Blazor.Client.Models
{
    public class WsResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}

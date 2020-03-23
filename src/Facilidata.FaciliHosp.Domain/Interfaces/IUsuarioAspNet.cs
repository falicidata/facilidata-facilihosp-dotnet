namespace Facilidata.FaciliHosp.Domain.Interfaces
{
    public interface IUsuarioAspNet
    {
        string GetUserName();
        bool IsAuthenticated();
    }
}

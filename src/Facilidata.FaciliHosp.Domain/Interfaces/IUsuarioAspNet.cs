using System.Collections.Generic;
using System.Security.Claims;

namespace Facilidata.FaciliHosp.Domain.Interfaces
{
    public interface IUsuarioAspNet
    {
        string GetUserName();
        bool IsAuthenticated();
        string GetUsuarioId();
        List<Claim> GetClaims();
    }
}

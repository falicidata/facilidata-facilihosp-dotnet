using Facilidata.FaciliHosp.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Facilidata.FaciliHosp.Infra.Identity.Services
{
    public class UsuarioAspNet : IUsuarioAspNet
    {
        private readonly IHttpContextAccessor _accessor;

        public UsuarioAspNet(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string GetUserName()
        {
            return _accessor.HttpContext.User.Identity.Name;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}

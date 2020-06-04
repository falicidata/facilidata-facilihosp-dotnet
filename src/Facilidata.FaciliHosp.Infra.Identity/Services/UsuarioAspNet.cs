using Facilidata.FaciliHosp.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
            return _accessor.HttpContext?.User?.Identity?.Name;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }


        public string GetUsuarioId()
        {
            return _accessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "UsuarioId")?.Value;
        }

        public List<Claim> GetClaims()
        {
            return _accessor.HttpContext?.User?.Claims?.ToList();
        }
    }
}

using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Enums;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Infra.Identity.Claims
{
    public class UsuarioClaimsPrincipalFactory : UserClaimsPrincipalFactory<Usuario>
    {
        private readonly ContextIdentity _contextIdentity;
        public UsuarioClaimsPrincipalFactory(UserManager<Usuario> userManager, IOptions<IdentityOptions> optionsAccessor, ContextIdentity contextIdentity) : base(userManager, optionsAccessor)
        {
            _contextIdentity = contextIdentity;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Usuario user)
        {
            var identityClaims = await base.GenerateClaimsAsync(user);

            var ePaciente = _contextIdentity.Pacientes.Find(user.ContaId) != null;
            ETipoUsuario tipo = ePaciente ? ETipoUsuario.Paciente : ETipoUsuario.Medico;

            var claimTipo = new Claim("TipoUsuario", tipo.ToString());
            var claimUsuarioId = new Claim("UsuarioId", user.Id);

            identityClaims.AddClaim(claimTipo);
            identityClaims.AddClaim(claimUsuarioId);

            return identityClaims;

        }
    }
}

using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Application.ClaimsFactory
{
    public class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<Usuario>
    {
        private readonly IContaRepository _contaRepository;
        public ClaimsPrincipalFactory(UserManager<Usuario> userManager, IOptions<IdentityOptions> optionsAccessor, IContaRepository contaRepository) : base(userManager, optionsAccessor)
        {
            _contaRepository = contaRepository;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Usuario user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var conta = _contaRepository.ObterPorId(user.ContaId);
            identity.AddClaim(new Claim("UserName", conta.Nome ?? null));
            return identity;
        }
    }
}

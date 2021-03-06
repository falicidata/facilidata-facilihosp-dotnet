﻿using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Infra.Identity.Claims
{
    public class UsuarioClaimsPrincipalFactory : UserClaimsPrincipalFactory<Usuario>
    {
        private readonly ContextIdentity _contextIdentity;
        private readonly IContaRepository _contaRepository;
        public UsuarioClaimsPrincipalFactory(UserManager<Usuario> userManager, IOptions<IdentityOptions> optionsAccessor, ContextIdentity contextIdentity, IContaRepository contaRepository) : base(userManager, optionsAccessor)
        {
            _contextIdentity = contextIdentity;
            _contaRepository = contaRepository;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Usuario user)
        {
            var identityClaims = await base.GenerateClaimsAsync(user);

            var claimUsuarioId = new Claim("UsuarioId", user.Id);
          
            var conta = _contaRepository.ObterPorId(user.ContaId);
            string primeiroNome = conta.Nome == null ? null : conta.Nome.Split(' ')[0];
            var claimUsuarioNome = new Claim("UserName", primeiroNome);
            identityClaims.AddClaim(claimUsuarioId);
            identityClaims.AddClaim(claimUsuarioNome);

            return identityClaims;

        }
    }
}

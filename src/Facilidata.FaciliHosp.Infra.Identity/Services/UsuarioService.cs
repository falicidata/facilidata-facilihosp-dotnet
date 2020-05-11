using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Enums;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Infra.Identity.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IContaRepository _contaRepository;
        private readonly IUnitOfWork<ContextIdentity> _uow;
        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,  IUnitOfWork<ContextIdentity> uow, IContaRepository contaRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _uow = uow;
            _contaRepository = contaRepository;
        }




        public async Task<IdentityResult> Registro(RegistroViewModel viewModel)
        {
            var conta = new Conta(viewModel.Nome, Enum.Parse<ESexoConta>(viewModel.Sexo),viewModel.DataNascimento,viewModel.CRM);
            var usuario = new Usuario(viewModel.Email, conta.Id);

            _contaRepository.Inserir(conta);
            var resultadoCommit = _uow.Commit();

            if (resultadoCommit == false) return null;

            var resultadoIdentity = await _userManager.CreateAsync(usuario, viewModel.Senha);
            if (!resultadoIdentity.Succeeded)
            {
                _contaRepository.Deletar(conta.Id);
                return resultadoIdentity;
            }

            return resultadoIdentity;
        }


        public async Task<bool> Login(LoginUsuarioViewModel viewModel)
        {
            var resultadoLogin = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Senha, false, false);
            return resultadoLogin.Succeeded;

        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();

        }
    }
}

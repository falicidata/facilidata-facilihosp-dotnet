using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Infra.Identity.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IUnitOfWork<ContextIdentity> _uow;
        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository, IUnitOfWork<ContextIdentity> uow)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
            _uow = uow;
        }

        public async Task<IdentityResult> Registro(RegistroUsuarioViewModel viewModel)
        {
            var tipo = viewModel.Tipo.Value;

            if (tipo == Enums.ETipoUsuario.Medico)
            {
                var medico = new Medico();
                var usuario = new Usuario(viewModel.Email, medico.Id);

                _medicoRepository.Inserir(medico);
                var resultadoCommit = _uow.Commit();

                if (resultadoCommit == false) return null;

                var resultadoIdentity = await _userManager.CreateAsync(usuario, viewModel.Senha);
                if (!resultadoIdentity.Succeeded)
                {
                    _medicoRepository.Deletar(medico.Id);
                    return resultadoIdentity;
                }
                return resultadoIdentity;


            }
            else // Paciente
            {
                var paciente = new Paciente();
                var usuario = new Usuario(viewModel.Email, paciente.Id);

                _pacienteRepository.Inserir(paciente);
                var resultadoCommit = _uow.Commit();

                if (resultadoCommit == false) return null;
                if (resultadoCommit == true)
                {
                    var resultadoIdentity = await _userManager.CreateAsync(usuario, viewModel.Senha);
                    if (!resultadoIdentity.Succeeded)
                    {
                        _pacienteRepository.Deletar(paciente.Id);
                        return resultadoIdentity;
                    }

                    return resultadoIdentity;
                }
            }

            return null;
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

using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Enums;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ContextIdentity _contextIdentity;
        private readonly IUsuarioAspNet _usuarioAspNet;
        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository, IUnitOfWork<ContextIdentity> uow, ContextIdentity contextIdentity, IUsuarioAspNet usuarioAspNet)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
            _uow = uow;
            _contextIdentity = contextIdentity;
            _usuarioAspNet = usuarioAspNet;
        }


        public List<Paciente> ObterPacientes()
        {
            return _contextIdentity.Pacientes.Include(paciente => paciente.Usuario).Where(paciente => !paciente.Deletado).ToList();
        }

        public async Task<IdentityResult> RegistroMedico(RegistroMedicoViewModel viewModel)
        {
            var medico = new Medico() { CRM = viewModel.CRM , Sexo = Enum.Parse<ESexoConta>(viewModel.Sexo) };
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


        public async Task<IdentityResult> RegistroPaciente(RegistroPacienteViewModel viewModel)
        {
            var paciente = new Paciente()
            {
                CPF = viewModel.CPF,
                ConvenioMedico = viewModel.ConvenioMedico,
                Sexo = Enum.Parse<ESexoConta>(viewModel.Sexo)
            };
            var usuario = new Usuario(viewModel.Email, paciente.Id);

            _pacienteRepository.Inserir(paciente);
            var resultadoCommit = _uow.Commit();

            if (resultadoCommit == false) return null;

            var resultadoIdentity = await _userManager.CreateAsync(usuario, viewModel.Senha);
            if (!resultadoIdentity.Succeeded)
            {
                _pacienteRepository.Deletar(paciente.Id);
                return resultadoIdentity;
            }

            return resultadoIdentity;

        }

        public ETipoUsuario? GetTipoUsuarioLogado()
        {
            var claimsUsuario = _usuarioAspNet.GetClaims();
            var usuarioTipoClaim = claimsUsuario.FirstOrDefault(claim => claim.Type == "TipoUsuario")?.Value;
            if (string.IsNullOrEmpty(usuarioTipoClaim)) return null;
            else return Enum.Parse<ETipoUsuario>(usuarioTipoClaim);
        }

        public ETipoUsuario GetTipoUsuario(Usuario usuario)
        {
            var conta = _pacienteRepository.ObterPorId(usuario.ContaId);
            return conta != null ? ETipoUsuario.Paciente : ETipoUsuario.Medico;
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

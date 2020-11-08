using AutoMapper;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Enums;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Infra.Identity.Services
{
    public class UsuarioService :  IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IContaRepository _contaRepository;
        private readonly IUnitOfWork<ContextIdentity> _uow;
        private readonly IUsuarioAspNet _usuarioAspNet;
        private readonly IHttpContextAccessor _accessor;
        protected readonly IMapper _mapper;

        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IMapper mapper,
            IUnitOfWork<ContextIdentity> uow, IContaRepository contaRepository, IUsuarioAspNet usuarioAspNet, IHttpContextAccessor accessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _uow = uow;
            _contaRepository = contaRepository;
            _usuarioAspNet = usuarioAspNet;
            _mapper = mapper;
            _accessor = accessor;
        }



        public List<Usuario> ObterTodos()
        {
            return _userManager.Users.ToList();
        }


        private IdentityResult ValidaRegistro(RegistroViewModel viewModel)
        {
             bool emailJaExiste = _userManager.Users.FirstOrDefault(u => u.Email == viewModel.Email) != null;
            if (emailJaExiste)
                return IdentityResult.Failed(new IdentityError() { Description = "E-mail já cadastrado, tente outro e-mail" });
            

            bool cpfJaExist = _contaRepository.Pesquisar(c => c.Cpf == viewModel.CPF).Any();
            if (cpfJaExist)
                return IdentityResult.Failed(new IdentityError() { Description = "CPF já cadastrado" });

            return IdentityResult.Success;
        }


        public async Task<IdentityResult> Registro(RegistroViewModel viewModel)
        {
            var result =  ValidaRegistro(viewModel);
            if (!result.Succeeded) return result;

            var conta = new Conta(viewModel.Nome, Enum.Parse<ESexoConta>(viewModel.Sexo),viewModel.DataNascimento,viewModel.CPF, viewModel.PlanoId);
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

        public AlteracaoViewModel ObterPorId()
        {
            string usuarioId = _usuarioAspNet.GetUsuarioId();

            var conta = _contaRepository.Pesquisar(x => x.Usuario.Id == usuarioId).FirstOrDefault();
            if (conta == null) return null;
            var viewModel = _mapper.Map<AlteracaoViewModel>(conta);
            viewModel.Email = _usuarioAspNet.GetUserName();
            viewModel.IdUsuario = _usuarioAspNet.GetUsuarioId();
            return viewModel;
        }

        public async Task<bool> Salvar(AlteracaoViewModel viewModel)
        {
            var conta = new Conta(viewModel.Nome, Enum.Parse<ESexoConta>(viewModel.Sexo), viewModel.DataNascimento, viewModel.CPF, viewModel.PlanoId);
            conta.Id = viewModel.Id;
            var usuario = await _userManager.FindByIdAsync(viewModel.IdUsuario);

            _contaRepository.Atualizar(viewModel.Id, conta);
            var resultadoCommit = _uow.Commit();

            if (resultadoCommit == false) return resultadoCommit;     

            if (!String.IsNullOrEmpty(viewModel.Senha))
            {
                var resultadoIdentity = await _userManager.ChangePasswordAsync(usuario, viewModel.SenhaAntiga, viewModel.Senha);
                return resultadoIdentity.Succeeded;
            }
            return true;
        }

        public void AlterarPlano(string idPlano)
        {
            string usuarioId = _usuarioAspNet.GetUsuarioId();
            var conta = _contaRepository.Pesquisar(x => x.Usuario.Id == usuarioId).FirstOrDefault();
            conta.PlanoId = idPlano;
            _contaRepository.Atualizar(conta.Id, conta);
            _uow.Commit();
        }

        public string ObterPlano()
        {
            string usuarioId = _usuarioAspNet.GetUsuarioId();
            var conta = _contaRepository.Pesquisar(x => x.Usuario.Id == usuarioId).FirstOrDefault();
            return conta.PlanoId;
        }

    }
}

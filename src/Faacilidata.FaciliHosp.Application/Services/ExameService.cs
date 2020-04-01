﻿using AutoMapper;
using Azure.Storage.Blobs;
using Facilidata.FaciliHosp.Application.Interfaces;
using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Application.Services
{
   public class ExameService : Service, IExameService
    {
        private readonly IExameRepository _exameRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IAzureStorageService _azureStorageService;

        public ExameService(IUnitOfWork<ContextSQL> uow, IMapper mapper, IActionContextAccessor actionContextAccessor, IExameRepository exameRepository, IUsuarioService usuarioService, IAzureStorageService azureStorageService) : base(uow, mapper, actionContextAccessor)
        {
            _exameRepository = exameRepository;
            _usuarioService = usuarioService;
            _azureStorageService = azureStorageService;
        }

        public void RemoverAnexo(EditarExameViewModel viewModel)
        {
            var exame = _exameRepository.ObterPorId(viewModel.Id);
            _azureStorageService.Deletar(exame.Url);
            if (ExisteErrosNoModelState()) return;

            exame.ContentType = null;
            exame.NomeArquivo = null;
            exame.Url = null;
            _exameRepository.Atualizar(viewModel.Id,exame);
            _uow.Commit();
        }

        public bool Deletar(string id)
        {
            _exameRepository.Deletar(id);
            return _uow.Commit();
        }

        public EditarExameViewModel Editar(string id,string hospitalId,string usuarioId)
        {
            if (string.IsNullOrEmpty(id))
                return new EditarExameViewModel() { HospitalId = hospitalId, UsuarioId = usuarioId };

            var exame = _exameRepository.ObterPorId(id);
            if (exame == null) return null;
            var viewModel = _mapper.Map<EditarExameViewModel>(exame);
            return viewModel;
        }

        public ExameIndexViewModel ObterExamesPorHospitaIdEUsuarioId(string hospitalId, string usuarioId)
        {
            var exames = _exameRepository.ObterTodosSemAnexoPorHospitalIdEUsuarioId(hospitalId, usuarioId);
            var viewModel = new ExameIndexViewModel() { Exames = exames, HospitalId = hospitalId, UsuarioId = usuarioId };
            return viewModel;
        }

        public List<ExamePacientesViewModel> ObterPacientes(string hospitalId)
        {
            var pacientes = _usuarioService.ObterPacientes();
            var viewModel = pacientes.Select(paciente => new ExamePacientesViewModel() { HospitalId = hospitalId, Nome = paciente.Usuario.UserName, Id = paciente.Usuario.Id }).ToList();
            return viewModel;
        }

        public   bool Salvar(EditarExameViewModel viewModel)
        {

            if (string.IsNullOrEmpty(viewModel.Id))
            {
                var novoExame = _mapper.Map<Exame>(viewModel);
                
                if(viewModel.Upload != null)
                {
                    novoExame.NomeArquivo = viewModel.Upload.FileName;
                    novoExame.ContentType = viewModel.Upload.ContentType;
                    string path = _azureStorageService.Upload(viewModel.Upload.OpenReadStream(), viewModel.Upload.FileName);
                    if (ExisteErrosNoModelState()) return false;
                    novoExame.Url = path;
                }

                _exameRepository.Inserir(novoExame);
                var novoResultado = _uow.Commit();
                return novoResultado;
            }

            var exame = _exameRepository.ObterPorId(viewModel.Id);
            if (exame == null) return false;
            var exameAtualizado = _mapper.Map<Exame>(viewModel);
            if (viewModel.Upload != null)
            {
                exameAtualizado.NomeArquivo = viewModel.Upload.FileName;
                exameAtualizado.ContentType = viewModel.Upload.ContentType;
                string path = _azureStorageService.Upload(viewModel.Upload.OpenReadStream(), viewModel.Upload.FileName);
                if (ExisteErrosNoModelState()) return false;
                exameAtualizado.Url = path;
            }
           
            _exameRepository.Atualizar(viewModel.Id, exameAtualizado);
            var atualizacaoResultado = _uow.Commit();
            return atualizacaoResultado;
        }
    }
}

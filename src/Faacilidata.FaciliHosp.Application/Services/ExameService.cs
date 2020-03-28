using AutoMapper;
using Facilidata.FaciliHosp.Application.Interfaces;
using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Facilidata.FaciliHosp.Application.Services
{
   public class ExameService : Service, IExameService
    {
        private readonly IExameRepository _exameRepository;
        private readonly IUsuarioService _usuarioService;

        public ExameService(IUnitOfWork<ContextSQL> uow, IMapper mapper, IExameRepository exameRepository, IUsuarioService usuarioService) : base(uow, mapper)
        {
            _exameRepository = exameRepository;
            _usuarioService = usuarioService;
        }

        public void RemoverAnexo(EditarExameViewModel viewModel)
        {
            var exame = _exameRepository.ObterPorId(viewModel.Id);
            exame.ContentType = null;
            exame.Anexo = new byte[] { };
            exame.NomeArquivo = null;
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

        private byte[] ConverteStreamToByteArray(Stream stream)
        {
            byte[] byteArray = new byte[16 * 1024];
            using (MemoryStream mStream = new MemoryStream())
            {
                int bit;
                while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
                {
                    mStream.Write(byteArray, 0, bit);
                }
                return mStream.ToArray();
            }
        }

        public bool Salvar(EditarExameViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Id))
            {
                var novoExame = _mapper.Map<Exame>(viewModel);
                
                if(viewModel.Upload != null)
                {
                    novoExame.NomeArquivo = viewModel.Upload.FileName;
                    novoExame.ContentType = viewModel.Upload.ContentType;
                    novoExame.Anexo = ConverteStreamToByteArray(viewModel.Upload.OpenReadStream());
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
                exameAtualizado.Anexo = ConverteStreamToByteArray(viewModel.Upload.OpenReadStream());
            }
            if (viewModel.Upload == null && exame.Anexo != exameAtualizado.Anexo) exameAtualizado.Anexo = exame.Anexo;
            _exameRepository.Atualizar(viewModel.Id, exameAtualizado);
            var atualizacaoResultado = _uow.Commit();
            return atualizacaoResultado;
        }
    }
}

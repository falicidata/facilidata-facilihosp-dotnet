    using AutoMapper;
using Facilidata.FaciliHosp.Application.Interfaces;
using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Domain.Models;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Tesseract;


namespace Facilidata.FaciliHosp.Application.Services
{
    public class ExameService : Service, IExameService
    {
        private readonly IExameRepository _exameRepository;
        private readonly IExameTipoRepository _exameTipoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IAzureStorageService _azureStorageService;
        private readonly IUsuarioAspNet _usuarioAspNet;

        public ExameService(IUnitOfWork<ContextSQL> uow, IMapper mapper, IActionContextAccessor actionContextAccessor, IExameRepository exameRepository, IUsuarioService usuarioService, IAzureStorageService azureStorageService, IUsuarioAspNet usuarioAspNet, IExameTipoRepository exameTipoRepository) : base(uow, mapper, actionContextAccessor)
        {
            _exameRepository = exameRepository;
            _usuarioService = usuarioService;
            _azureStorageService = azureStorageService;
            _usuarioAspNet = usuarioAspNet;
            _exameTipoRepository = exameTipoRepository;
        }

        public void RemoverAnexo(EditarExameViewModel viewModel)
        {
            var exame = _exameRepository.ObterPorId(viewModel.Id);
            _azureStorageService.Deletar(exame.Url);
            if (ExisteErrosNoModelState()) return;

            exame.ContentType = null;
            exame.NomeArquivo = null;
            exame.Url = null;
            _exameRepository.Atualizar(viewModel.Id, exame);
            _uow.Commit();
        }

        public bool Deletar(string id)
        {
            _exameRepository.Deletar(id);
            return _uow.Commit();
        }

        public EditarExameViewModel Editar(string id)
        {
            string usuarioId = _usuarioAspNet.GetUsuarioId();

            if (string.IsNullOrEmpty(id))
                return new EditarExameViewModel() { UsuarioId = usuarioId };

            var exame = _exameRepository.ObterPorId(id);
            if (exame == null) return null;
            var viewModel = _mapper.Map<EditarExameViewModel>(exame);
            return viewModel;
        }

        public List<ExameSemAnexo> ObterExamesUsuarioLogado()
        {
            string usuarioId = _usuarioAspNet.GetUsuarioId();
            if (string.IsNullOrEmpty(usuarioId)) return new List<ExameSemAnexo>();
            return _exameRepository.ObterTodosSemAnexoPorUsuarioId(usuarioId);
        }
        public static Bitmap ByteToImage(byte[] blob)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                mStream.Write(blob, 0, blob.Length);
                mStream.Seek(0, SeekOrigin.Begin);

                Bitmap bm = new Bitmap(mStream);
                return bm;
            }
        }
        public byte[] StreamToArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public string LerArquivoImagem(string pathImage)
        {

            try
            {
                string directory = Directory.GetCurrentDirectory();
                var engine = new TesseractEngine("./tessdata", "por", EngineMode.Default);
                var pix = Pix.LoadFromFile(pathImage);
                var page = engine.Process(pix, PageSegMode.Auto);
                var meanConfidence = page.GetMeanConfidence();
                var text = page.GetText();
                var indexTrial = text.IndexOf("notice!)") + 8;
                string subs = text.Substring(indexTrial + 1);
                pix.Dispose();
                engine.Dispose();
                return subs;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return $"Erro ao tentar ler resultado do documento enviado, erro: {e.Message}";
            }
        }


        public string LerArquivoPdf(Stream streamPdf)
        {

            string pdfPath = Path.Combine(Directory.GetCurrentDirectory(), "tmp", $"tmp-pdf-{Guid.NewGuid()}.pdf");

            using (var fileStream = File.Create(pdfPath))
            {
                streamPdf.Seek(0, SeekOrigin.Begin);
                streamPdf.CopyTo(fileStream);
            }

            try
            {

                SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
                f.OpenPdf(pdfPath);
                int pageCount = f.PageCount;

                if (f.PageCount > 0)
                {
                    //Set image properties: Jpeg, 200 dpi
                    f.ImageOptions.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
                    f.ImageOptions.Dpi = 300;

                    string pathImage = Path.Combine(Directory.GetCurrentDirectory(), "tmp");
                    string nameImage = $"tmp-img-{Guid.NewGuid()}_";

                    try
                    {
                        //Save all PDF pages as page1.jpg, page2.jpg ... pageN.jpg
                        f.ToImage(Path.Combine(Directory.GetCurrentDirectory(), "tmp"), nameImage);

                  
                        string res = LerArquivoImagem(Path.Combine(pathImage, nameImage + "1.jpg"));
                        f.ClosePdf();

                        File.Delete(pdfPath);
                        File.Delete(Path.Combine(pathImage, nameImage + "1.jpg"));
                        return res;
                    }
                    catch(Exception e)
                    {

                        File.Delete(pdfPath);
                        File.Delete(Path.Combine(pathImage, nameImage + "1.jpg"));
                        Debug.WriteLine(e.Message);
                        return $"Erro ao tentar ler resultado do documento enviado, erro: {e.Message}";
                    }

               
                }

                return "";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return $"Erro ao tentar ler resultado do documento enviado, erro: {e.Message}";
            }
        }

        private string ObterTextoDoExameSePdf(EditarExameViewModel viewModel)
        {
            string resultado = "";
            if (viewModel.Formato == Domain.Enums.EFormatoExame.Laudo && viewModel.Upload.FileName.IndexOf(".pdf") >= 0)
                resultado = LerArquivoPdf(viewModel.Upload.OpenReadStream());

            return resultado;
        }

        public bool Salvar(EditarExameViewModel viewModel)
        {
            if (viewModel.Upload != null)
            {
                var tamanho = viewModel.Upload.Length / 1000000;
                if (tamanho > 30)
                {
                    AdicionarErroModelState("Seu plano não suporta arquivos maiores de 30mb, por favor escolha um arquivo menor");
                    return false;
                }
            }

           var exameTipo = _exameTipoRepository.InsereSeNaoExistir(viewModel.TipoOutro);

            if (string.IsNullOrEmpty(viewModel.Id))
            {
                var novoExame = _mapper.Map<Exame>(viewModel);
                novoExame.TipoId = exameTipo.Id;
                novoExame.TipoOutro = exameTipo.Nome;

                if (viewModel.Upload != null)
                {


                    novoExame.NomeArquivo = viewModel.Upload.FileName;
                    novoExame.ContentType = viewModel.Upload.ContentType;
                    string path = _azureStorageService.Upload(viewModel.Upload.OpenReadStream(), viewModel.Upload.FileName);
                    if (ExisteErrosNoModelState()) return false;
                    novoExame.Url = path;
                    novoExame.Resultado = ObterTextoDoExameSePdf(viewModel);
                }

                _exameRepository.Inserir(novoExame);
                var novoResultado = _uow.Commit();
                return novoResultado;
            }

            var exame = _exameRepository.ObterPorId(viewModel.Id);
            if (exame == null) return false;
            var exameAtualizado = _mapper.Map<Exame>(viewModel);
            exameAtualizado.TipoId = exameTipo.Id;
            exameAtualizado.TipoOutro = exameTipo.Nome;
            if (viewModel.Upload != null)
            {
                exameAtualizado.NomeArquivo = viewModel.Upload.FileName;
                exameAtualizado.ContentType = viewModel.Upload.ContentType;
                string path = _azureStorageService.Upload(viewModel.Upload.OpenReadStream(), viewModel.Upload.FileName);
                exameAtualizado.Resultado = ObterTextoDoExameSePdf(viewModel);
                if (ExisteErrosNoModelState()) return false;
                exameAtualizado.Url = path;
            }

            _exameRepository.Atualizar(viewModel.Id, exameAtualizado);
            var atualizacaoResultado = _uow.Commit();
            return atualizacaoResultado;
        }

        public List<TipoExame> ObterExamesTipos()
        {
            return _exameRepository.ObterExamesTipos();
        }
    }
}

using AutoMapper;
using Azure.Storage.Blobs;
using Facilidata.FaciliHosp.Application.Interfaces;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Facilidata.FaciliHosp.Application.Services
{
    public class AzureStorageService : Service, IAzureStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        private readonly IUsuarioAspNet _usuarioAspNet;

        protected string UsuarioId { get { return _usuarioAspNet.GetUsuarioId(); } }
        public AzureStorageService(IUnitOfWork<ContextSQL> uow, IMapper mapper, IActionContextAccessor actionContextAccessor, IUsuarioAspNet usuarioAspNet) : base(uow, mapper, actionContextAccessor)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetSection("AzureStorageBlob:ConnectionString").Value;
            string container = configuration.GetSection("AzureStorageBlob:Container").Value;

            _blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient(container);
            _usuarioAspNet = usuarioAspNet;
        }

        public bool Deletar(string path)
        {
            try
            {
                string nomeArquivoBlob = Path.GetFileName(path);
                var container = _blobServiceClient.GetBlobContainerClient(UsuarioId);
                var result = container.DeleteBlobIfExists(nomeArquivoBlob);
                return result.Value;
            }
            catch (Exception e)
            {
                AdicionarErroModelState("AzureStorageService", $"Erro ao deletar arquivo : Erro: {e.Message}");
                return false;
            }
        }

        public string Upload(Stream stream, string nomeArquivo)
        {
            try
            {
              
                var container = _blobServiceClient.GetBlobContainerClient(UsuarioId);
                var res = container.CreateIfNotExists( Azure.Storage.Blobs.Models.PublicAccessType.Blob);

                string path = container.Uri.AbsoluteUri + "/" + nomeArquivo;
                var resDelete = container.DeleteBlobIfExistsAsync(nomeArquivo).GetAwaiter().GetResult();
                
                container.UploadBlob(nomeArquivo, stream);
                return path;
            }
            catch (Exception e)
            {
                AdicionarErroModelState($"Erro ao fazer upload do arquivo : Erro: {e.Message}","AzureStorageService");
                return null;
            }
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

        public byte[] DownloadToBytes(string path)
        {
            try
            {
                var container = _blobServiceClient.GetBlobContainerClient(UsuarioId);
                string nomeArquivoBlob = Path.GetFileName(path);
                var blobClient = container.GetBlobClient(nomeArquivoBlob);
                var response = blobClient.Download();
                Stream streamDownload = response.Value.Content;
                var bytesArquivo = ConverteStreamToByteArray(streamDownload);
                return bytesArquivo;
            }
            catch (Exception e)
            {
                AdicionarErroModelState("AzureStorageService", $"Erro ao fazer download do arquivo : Erro: {e.Message}");
                return null;
            }
        }

        public string DownloadToBase64(string path)
        {
            try
            {
                string nomeArquivoBlob = Path.GetFileName(path);
                var blobClient = _containerClient.GetBlobClient(nomeArquivoBlob);
                var response = blobClient.Download();
                Stream streamDownload = response.Value.Content;
                var bytesArquivo = ConverteStreamToByteArray(streamDownload);
                var base64Arquivo = Convert.ToBase64String(bytesArquivo);
                return base64Arquivo;
            }
            catch (Exception e)
            {
                AdicionarErroModelState("AzureStorageService", $"Erro ao fazer download do arquivo : Erro: {e.Message}");
                return null;
            }
        }
    }
}

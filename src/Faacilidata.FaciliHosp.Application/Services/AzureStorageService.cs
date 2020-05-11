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
 

        public AzureStorageService(IUnitOfWork<ContextSQL> uow, IMapper mapper, IActionContextAccessor actionContextAccessor) : base(uow, mapper, actionContextAccessor)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetSection("AzureStorageBlob:ConnectionString").Value;
            string container = configuration.GetSection("AzureStorageBlob:Container").Value;

            _blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient(container);
        
        }

        public bool Deletar(string path)
        {
            try
            {
                string nomeArquivoBlob = Path.GetFileName(path);
              
                var result = _containerClient.DeleteBlobIfExists(nomeArquivoBlob);
         
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
                string nomeArquivoStorage = $"{Guid.NewGuid().ToString()}_{nomeArquivo}";
                string path = Path.Combine(_containerClient.Uri.AbsoluteUri, nomeArquivoStorage);
                _containerClient.UploadBlob(nomeArquivoStorage, stream);
                return path;
            }
            catch (Exception e)
            {
                AdicionarErroModelState("AzureStorageService", $"Erro ao fazer upload do arquivo : Erro: {e.Message}");
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
                string nomeArquivoBlob = Path.GetFileName(path);
                var blobClient = _containerClient.GetBlobClient(nomeArquivoBlob);
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

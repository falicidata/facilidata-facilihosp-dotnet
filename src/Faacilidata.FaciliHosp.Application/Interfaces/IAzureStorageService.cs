using System.IO;

namespace Facilidata.FaciliHosp.Application.Interfaces
{
    public interface IAzureStorageService
    {
        string DownloadToBase64(string path);
        byte[] DownloadToBytes(string path, string usuarioId = null);
        string Upload(Stream stream, string nomeArquivo);
        bool Deletar(string path);
    }
}

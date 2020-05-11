using Facilidata.FaciliHosp.Domain.Enums;

namespace Facilidata.FaciliHosp.Domain.Entidades
{
    public class Exame : Entidade
    {
        protected Exame() { }
        public Exame(string usuarioId, string tipoOutro, string resultado, string url, EFormatoExame formato, string tipoId, string fornecedorId, string fornecedor)
        {
            UsuarioId = usuarioId;
            TipoOutro = tipoOutro;
            Resultado = resultado;
            Url = url;
            TipoId = tipoId;
            FornecedorId = fornecedorId;
            Formato = formato;
            Fornecedor = fornecedor;
        }

        public EFormatoExame Formato { get; set; }
        public string UsuarioId { get; set; }
        public string FornecedorId { get; set; }
        public string TipoId { get; set; }
        public string TipoOutro { get; set; }
        public string Fornecedor { get; set; }
        public string Resultado { get; set; }
        public string Url { get; set; }
        public string ContentType { get; set; }
        public string NomeArquivo { get; set; }

        // Entity Framework
        public ExameTipo Tipo { get; set; }
    }
}

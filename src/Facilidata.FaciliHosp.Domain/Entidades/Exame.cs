namespace Facilidata.FaciliHosp.Domain.Entidades
{
    public class Exame : Entidade
    {
        protected Exame() { }
        public Exame(string hospitalId, string userId, string tipo, string resultado, string url, byte[] anexo)
        {
            HospitalId = hospitalId;
            UsuarioId = userId;
            Tipo = tipo;
            Resultado = resultado;
            Url = url;
            Anexo = anexo;
        }

        public string HospitalId { get; set; }
        public string UsuarioId { get; set; }
        public string Tipo { get; set; }
        public string Resultado { get; set; }
        public string Url { get; set; }
        public byte[] Anexo { get; set; }

        // Entity Framework
        public Hospital Hospital { get; set; }
    }
}

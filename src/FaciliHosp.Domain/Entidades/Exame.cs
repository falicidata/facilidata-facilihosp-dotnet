using System;

namespace FaciliHosp.Domain.Entidades
{
    public class Exame : Entidade
    {
        protected Exame() { }
        public Exame(Guid? atendimentoId, Guid userId, Guid hospitalId, string tipo, string resultado, string url, string anexo)
        {
            AtendimentoId = atendimentoId;
            UserId = userId;
            HospitalId = hospitalId;
            Tipo = tipo;
            Resultado = resultado;
            Url = url;
            Anexo = anexo;
        }

        public Guid? AtendimentoId { get; set; }
        public Guid UserId { get; set; }
        public Guid HospitalId { get; set; }
        public string Tipo { get; set; }
        public string Resultado { get; set; }
        public string Url { get; set; }
        public string Anexo { get; set; }

        // Entity Framework
        public Hospital Hospital { get; set; }
        public Atendimento Atendimento { get; set; }




    }
}

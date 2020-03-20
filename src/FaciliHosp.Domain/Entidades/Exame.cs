using System;

namespace FaciliHosp.Domain.Entidades
{
    public class Exame : Entidade
    {
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

        public Guid? AtendimentoId  { get; private set; }
        public Guid UserId { get; private set; }
        public Guid HospitalId { get; private set; }
        public string Tipo { get; private set; }
        public string Resultado { get; private set; }
        public string Url { get; private set; }
        public string Anexo { get; private set; }

        // Entity Framework
        public Hospital Hospital { get; set; }
        public Atendimento Atendimento { get; set; }




    }
}

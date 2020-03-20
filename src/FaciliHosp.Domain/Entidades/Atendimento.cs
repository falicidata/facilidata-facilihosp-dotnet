using System;
using System.Collections.Generic;
using System.Text;

namespace FaciliHosp.Domain.Entidades
{
    public class Atendimento : Entidade
    {
        public Atendimento(DateTime? dataHora, string status, string codigo, string avaliacao, Guid pacienteId, Guid hospitalId, Guid pacientePlanoId)
        {
            DataHora = dataHora;
            Status = status;
            Codigo = codigo;
            Avaliacao = avaliacao;
            PacienteId = pacienteId;
            HospitalId = hospitalId;
            PacientePlanoId = pacientePlanoId;
        }

        public DateTime? DataHora { get; private set; }
        public string Status{ get; private set; }
        public string Codigo { get; private set; }
        public string Avaliacao { get; private set; }
        public Guid PacienteId { get; private set; }
        public Guid HospitalId { get; private set; }
        public Guid PacientePlanoId { get; private set; }

        // Entity Framework
        public Hospital Hospital { get; set; }
    }
}

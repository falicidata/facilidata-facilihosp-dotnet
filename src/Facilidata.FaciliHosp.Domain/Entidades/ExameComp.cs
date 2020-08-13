using Facilidata.FaciliHosp.Domain.Enums;
using System;

namespace Facilidata.FaciliHosp.Domain.Entidades
{
    public class ExameComp : Entidade
    {
        public ExameComp(string exameId, string key, EPeriodoComp periodo = EPeriodoComp.Hora,DateTime? expiraEm = null, string usuarioId = null)
        {
            ExameId = exameId;
            Key = key;
            Periodo = periodo;
            UsuarioId = usuarioId;
            ExpiraEm = expiraEm ?? DateTime.Now.AddHours(1);
        }

        protected ExameComp() { }

        public string  ExameId{ get; set; }
        public string Key { get; set; }
        public EPeriodoComp Periodo { get; set; } = EPeriodoComp.Hora;
        public string UsuarioId { get; set; }
        public DateTime? ExpiraEm { get; set; }


        // EntityFramework
        public Exame Exame { get; set; }
    }
}

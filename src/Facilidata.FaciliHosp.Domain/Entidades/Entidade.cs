﻿using System;

namespace Facilidata.FaciliHosp.Domain.Entidades
{
    public abstract class Entidade
    {
        public Entidade()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string CriadoPor { get; set; }
        public DateTime? CriadoEm { get; set; }
        public string AtualizadoPor { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public string DeletadoPor { get; set; }
        public DateTime? DeletadoEm { get; set; }
        public bool Deletado { get; set; }
    }


}

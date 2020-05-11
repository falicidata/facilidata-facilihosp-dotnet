using Facilidata.FaciliHosp.Domain.Entidades;
using Facilidata.FaciliHosp.Infra.Identity.Enums;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using System;

namespace Facilidata.FaciliHosp.Infra.Identity.Entidades
{
    public class Conta : Entidade
    {
        protected Conta() { }
        public Conta(string nome, ESexoConta sexo, DateTime? dataNascimento,string crm = null)
        {
            Nome = nome;
            Crm = crm;
            DataNascimento = dataNascimento;
            Sexo = sexo;
        }

        public string Nome { get; set; }
        public string Crm { get; set; }
        public DateTime? DataNascimento { get; set; }
        public ESexoConta Sexo { get; set; }

        // Entity Framework
        public Usuario Usuario { get; set; }
    }
}

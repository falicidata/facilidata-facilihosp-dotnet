using System;

namespace FaciliHosp.Domain.Entidades
{
    public class Hospital : Entidade
    {
        protected Hospital() { }
        public Hospital(string nome, string endereco, string cep, string cidade, string estado, Guid userId)
        {
            Nome = nome;
            Endereco = endereco;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
            UserId = userId;
        }

        public string Nome { get;  set; }
        public string Endereco { get;  set; }
        public string Cep { get;  set; }
        public string Cidade { get;  set; }
        public string Estado { get;  set; }
        public Guid UserId { get;  set; }
    }
}

using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Domain.Entidades
{

    public class Hospital : Entidade
    {
        protected Hospital() { }
        public Hospital(string nome, string endereco, string cep, string bairro, string cidade, string estado)
        {
            Nome = nome;
            Endereco = endereco;
            Cep = cep;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string UserId { get; set; }

        // Entity Framework
        public List<Exame> Exames { get; set; }
    }
}

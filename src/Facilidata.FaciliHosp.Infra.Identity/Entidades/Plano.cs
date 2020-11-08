using Facilidata.FaciliHosp.Domain.Entidades;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Infra.Identity.Entidades
{
    public class Plano : Entidade
    {
        protected Plano() { }
        public Plano(string descricao, double valor, int armazenamento, int quantidadeExameSangue, int quantidadeExameImagem)
        {
            Descricao = descricao;
            Valor = valor;
            Armazenamento = armazenamento;
            QuantidadeExameSangue = quantidadeExameSangue;
            QuantidadeExameImagem = quantidadeExameImagem;
        }

        public string Descricao { get; private set; }
        public double Valor { get; private set; }
        public int Armazenamento { get; private set; }
        public int QuantidadeExameSangue { get; private set; }
        public int QuantidadeExameImagem { get; private set; }

        public ICollection<Conta> Contas { get; set; }

    }
}
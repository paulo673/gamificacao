using gamificacao.Shared.Models.Enums;
using System.Collections;
using System.Collections.Generic;

namespace gamificacao.Shared.Models
{
    public class Carta
    {
        public Carta(string nome, string descricao, TipoDeCarta tipo, int pontuacao, int id = 0)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Tipo = tipo;
            Pontuacao = pontuacao;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public TipoDeCarta Tipo { get; private set; }
        public int Pontuacao { get; private set; }
        public bool Aprovada { get; private set; }
        public ICollection<VotacaoDeCarta> Votacoes { get; set; }
    }
}

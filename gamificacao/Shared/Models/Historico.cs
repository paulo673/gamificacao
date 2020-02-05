using System;

namespace gamificacao.Shared.Models
{
    public class Historico
    {
        protected Historico() { }
        public Historico(int timeId, int cartaId, string observacao, Edicao edicao, int id =0)
        {
            Id = id;
            TimeId = timeId;
            CartaId = cartaId;
            Observacao = observacao;
            Edicao = edicao;
            Data = DateTime.Now;
        }

        public int Id { get; private set; }
        public int TimeId { get; private set; }
        public int CartaId { get; private set; }
        public int EdicaoId { get; private set; }
        public string Observacao { get; private set; }
        public DateTime Data { get; private set; }
        public Time Time { get; private set; }
        public Carta Carta { get; private set; }
        public Edicao Edicao { get; private set; }
    }
}

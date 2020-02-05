using gamificacao.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gamificacao.Shared.Models
{
    public class VotacaoDeCarta
    {
        protected VotacaoDeCarta() { }
        public VotacaoDeCarta(int jogadorId, int cartaId, int id = 0)
        {
            Id = id;
            JogadorId = jogadorId;
            CartaId = cartaId;
        }

        public int Id { get; private set; }
        public int JogadorId { get; private set; }
        public int CartaId { get; private set; }
        public Jogador Jogador { get; private set; }
        public Carta Carta { get; private set; }

        public bool VerificarSeJogadorPodeVotar(List<VotacaoDeCarta> votacoes, int jogadorId, int cartaId)
        {
            var jaVotouEmCartaDeSorte = false;
            var jaVotouEmCartaDeAzar = false;

            var jogadorAindaNaoVotou = !votacoes.Any(x => x.JogadorId == jogadorId);

            if (jogadorAindaNaoVotou)
                return true;

            foreach (var votacao in votacoes.AsEnumerable().Where(x => x.JogadorId == jogadorId))
            {
                if (votacao.CartaId == cartaId)
                {
                    return false;
                }

                if (votacao.Carta.Tipo == TipoDeCarta.Sorte)
                {
                    jaVotouEmCartaDeSorte = true;
                }
                else if (votacao.Carta.Tipo == TipoDeCarta.Azar)
                {
                    jaVotouEmCartaDeAzar = true;
                }
            }

            if (jaVotouEmCartaDeSorte && jaVotouEmCartaDeAzar)
            {
                return false;
            }

            return true;
        }
    }
}

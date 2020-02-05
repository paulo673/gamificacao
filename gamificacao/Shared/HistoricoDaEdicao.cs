using System;
using System.Collections.Generic;
using System.Text;

namespace gamificacao.Shared
{
    public class HistoricoDaEdicao
    {
        public string Time { get; set; }
        public List<HistoricoDaEdicao> Historicos { get; set; }
        public int Carta { get; set; }
        public int Pontuacao { get; set; }
        public DateTime Data { get; set; }
    }
}

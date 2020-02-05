using System;
using System.Collections.Generic;
using System.Text;

namespace gamificacao.Shared.Models
{
    public class Jogador
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int TimeId { get; private set; }
        public Time Time { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace gamificacao.Shared.Models.Dtos
{
    public class CartaDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public int Pontuacao { get; set; }
    }
}

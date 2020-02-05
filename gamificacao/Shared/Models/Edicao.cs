using gamificacao.Shared.Models.Dtos;
using gamificacao.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace gamificacao.Shared.Models
{
    public class Edicao
    {
        protected Edicao() { }

        public Edicao(int numero, DateTime inicio, DateTime fim, int id = 0)
        {
            Id = id;
            Numero = numero;
            Inicio = inicio;
            Fim = fim;
            Status = StatusDaEdicao.Aberta;
        }

        public int Id { get; private set; }
        public int Numero { get; private set; }
        public DateTime Inicio { get; private set; }
        public DateTime Fim { get; private set; }
        public StatusDaEdicao Status { get; private set; }
        public ICollection<Historico> Historicos { get; private set; }

        public void DefinirNumeroDaEdicao(int numeroDaUltimaEdicao)
        {
            this.Numero = numeroDaUltimaEdicao + 1;
        }

        public void Encerrar()
        {
            this.Status = StatusDaEdicao.Encerrada;
        }

        public void RegistrarHistorico(HistoricoDto historicoDto)
        {
            var historico = new Historico(historicoDto.TimeId, historicoDto.CartaId, historicoDto.Observacao, this);

            this.Historicos.Add(historico);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gamificacao.Shared.Models;
using gamificacao.Shared.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gamificacao.Server.Controllers
{
    [ApiController]
    public class VotacaoDeCartaController : ControllerBase
    {
        private readonly GamificacaoContexto contexto;

        public VotacaoDeCartaController(GamificacaoContexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet, Route("cartas-para-votar")]
        public async Task<List<Carta>> Get()
        {
            return await contexto.Cartas
                .Where(x => x.Aprovada == false)
                .ToListAsync();
        }

        [HttpPost, Route("votar")]
        public async Task<IActionResult> Votar(VotacaoDeCartaDto votacaoDeCartaDto)
        {
            var votacoes = await contexto.VotacoesDeCartas
                .Include(x => x.Carta)
                .ToListAsync();

            var votacao = new VotacaoDeCarta(votacaoDeCartaDto.JogadorId, votacaoDeCartaDto.CartaId);

            var jogadorPodeVotar = votacao.VerificarSeJogadorPodeVotar(votacoes, votacaoDeCartaDto.JogadorId, votacaoDeCartaDto.CartaId);

            if (jogadorPodeVotar)
            {
                contexto.Add(votacao);
                await contexto.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest("VocÊ já realizou seus votos");
            }
        }

        [HttpGet, Route("votacoes-de-cartas")]
        public async Task<IActionResult> Votacao()
        {
            var cartas = await contexto.Cartas
                .Include(x => x.Votacoes)
                .Where(x => x.Aprovada == false)
                .ToListAsync();

            var cartasVotadas = cartas.Select(carta => new
            {
                Id = carta.Id,
                Carta = carta.Nome,
                Tipo = carta.Tipo.ToString(),
                TotalDeVotos = carta.Votacoes.Count()
            }).ToList();

            return new ObjectResult(cartasVotadas.Where(x => x.TotalDeVotos > 0));
        }
    }
}
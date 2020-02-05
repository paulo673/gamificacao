using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gamificacao.Shared;
using gamificacao.Shared.Models;
using gamificacao.Shared.Models.Dtos;
using gamificacao.Shared.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gamificacao.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EdicaoController : ControllerBase
    {
        private readonly GamificacaoContexto contexto;

        public EdicaoController(GamificacaoContexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<List<Edicao>> Get()
        {
            return await contexto.Edicoes.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarEdicao(Edicao edicao)
        {
            var edicoes = await contexto.Edicoes.ToListAsync();


            var possuiEdicaoAberta = edicoes.Any(x => x.Status == StatusDaEdicao.Aberta);

            if (possuiEdicaoAberta)
            {
                return BadRequest("Já existe uma edição aberta!");
            }

            var ultimaEdicao = edicoes.Count() > 0 ? edicoes.Max(x => x.Numero) : 0;

            edicao.DefinirNumeroDaEdicao(ultimaEdicao);

            contexto.Add(edicao);
            await contexto.SaveChangesAsync();

            if (edicao == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost, Route("encerrar")]
        public async Task<IActionResult> EncerrarEdicao()
        {
            var edicao = await contexto.Edicoes.FirstOrDefaultAsync(x => x.Status == StatusDaEdicao.Aberta);

            edicao.Encerrar();

            await contexto.SaveChangesAsync();

            return Ok();
        }

        [HttpPost, Route("registrar-historico")]
        public async Task<IActionResult> RegistrarHistorico(HistoricoDto historicoDto)
        {
            var edicao = await contexto.Edicoes
                .Include(x => x.Historicos)
                .FirstOrDefaultAsync(x => x.Status == StatusDaEdicao.Aberta);

            edicao.RegistrarHistorico(historicoDto);

            await contexto.SaveChangesAsync();

            return Ok();
        }

        [HttpGet, Route("placar")]
        public async Task<IActionResult> ObterPlacarGeral()
        {
            var edicao = await contexto.Edicoes
               .Include(x => x.Historicos)
                    .ThenInclude(x => x.Time)
                .Include(x => x.Historicos)
                    .ThenInclude(x => x.Carta)
               .FirstOrDefaultAsync(x => x.Status == StatusDaEdicao.Aberta);

            var times = await contexto.Times.ToListAsync();

            var placarGeral = times.Select(time => new
            {
                Time = time.Nome,
                Pontuacao = edicao.Historicos.Where(x => x.TimeId == time.Id).Sum(x => x.Carta.Pontuacao)
            });

            return new ObjectResult(placarGeral);
        }

        [HttpGet, Route("historico")]
        public async Task<IActionResult> ObterHistorico()
        {
            var edicao = await contexto.Edicoes
                .Include(x => x.Historicos)
                     .ThenInclude(x => x.Time)
                 .Include(x => x.Historicos)
                     .ThenInclude(x => x.Carta)
                .FirstOrDefaultAsync(x => x.Status == StatusDaEdicao.Aberta);

            var times = await contexto.Times.ToListAsync();

            var historico = times.Select(time => new
            {
                Time = time.Nome,
                Historico = edicao.Historicos.Select(historico => new
                {
                    Data = historico.Data,
                    Carta = historico.Carta.Nome,
                    Pontuacao = historico.Carta.Pontuacao
                })
            });

            //var a = edicao.Historicos.Select(x => new
            //{
            //    Rodada = edicao.
            //});

            //var historicos = new List<HistoricoDaEdicao>();

            //var rodadas = edicao.Historicos.ToList();

            //foreach (var historico in edicao.Historicos)
            //{
            //    historicos.Add(new HistoricoDaEdicao
            //    {

            //    });
            //}

            return new ObjectResult(historico);
            //return new ObjectResult(edicao);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gamificacao.Shared.Models;
using gamificacao.Shared.Models.Dtos;
using gamificacao.Shared.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gamificacao.Server.Controllers
{
    [ApiController]
    public class CartaController : ControllerBase
    {
        private readonly GamificacaoContexto contexto;
        public CartaController(GamificacaoContexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet, Route("cartas")]
        public async Task<List<Carta>> Get()
        {
            return await contexto.Cartas.ToListAsync();
        }

        [HttpPost, Route("sugerir-carta")]
        public async Task<IActionResult> SugerirCarta(CartaDto cartaDto)
        {
            var carta = new Carta(cartaDto.Nome, cartaDto.Descricao, Enum.Parse<TipoDeCarta>(cartaDto.Tipo), cartaDto.Pontuacao);

            contexto.Add(carta);
            await contexto.SaveChangesAsync();

            return Ok();
        }
    }
}
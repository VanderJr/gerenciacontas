using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciaContas.Models;

namespace GerenciaContas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacoesController : ControllerBase
    {
        private readonly Context _context;

        public TransacoesController(Context context)
        {
            _context = context;
        }

        // GET: api/Transacoes
        [HttpGet]
        public IEnumerable<Transacao> GetTransacoes()
        {
            return _context.Transacoes;
        }

        // GET: api/Transacoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransacao([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transacao = await _context.Transacoes.FindAsync(id);

            if (transacao == null)
            {
                return NotFound();
            }

            return Ok(transacao);
        }


        // POST: api/Transacoes
        [HttpPost]
        public async Task<IActionResult> PostTransacao([FromBody] Transacao transacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transacaoValida = transacao.TipoTransacao == TipoTransacao.Aporte ?
                ValidarAporte(transacao) :
                ValidarTransferencia(transacao);
            if (!transacaoValida)
                return BadRequest(ModelState);
            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransacao", new { id = transacao.Id }, transacao);
        }

        [HttpPut]
        public async Task<IActionResult> EstornarTransacao([FromQuery] Guid transacaoId)
        {
            if (!TransacaoExists(transacaoId))
                return BadRequest(transacaoId);
            var transacao = _context.Transacoes.FindAsync(transacaoId);
            (await transacao).StatusTransacao = StatusTransacao.Estornada;
            await _context.SaveChangesAsync();
            return Ok(transacao);
        }

        private bool ValidarTransferencia(Transacao transacao)
        {
            return transacao.TipoTransacao == TipoTransacao.Transferencia &&
                   transacao.Debitada?.ContaRaiz.Id == transacao.Creditada?.ContaRaiz.Id &&
                   transacao.Debitada?.StatusConta == StatusConta.Ativa &&
                   transacao.Creditada?.StatusConta == StatusConta.Ativa;


        }

        private bool ValidarAporte(Transacao transacao)
        {
            return transacao.TipoTransacao == TipoTransacao.Aporte &&
                    transacao.Creditada?.TipoConta == TipoConta.Matriz &&
                    transacao.Valor > 0 &&
                    transacao.Debitada == null &&
                    transacao.Creditada.StatusConta == StatusConta.Ativa;
        }


        private bool TransacaoExists(Guid id)
        {
            return _context.Transacoes.Any(e => e.Id == id);
        }
    }
}
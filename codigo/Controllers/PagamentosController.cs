using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraDeVeiculosApi.Models;
using LocadoraDeVeiculosApi.Data;
using System.Net;

namespace LocadoraDeVeiculosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentosController : ControllerBase
    {
        private readonly LocadoraContext _context;

        public PagamentosController(LocadoraContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os pagamentos.
        /// </summary>
        /// <returns>Uma lista de pagamentos.</returns>
        /// <response code="200">Retorna a lista de pagamentos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Pagamento>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Pagamento>>> GetPagamentos()
        {
            return await _context.Pagamentos.ToListAsync();
        }

        /// <summary>
        /// Obtém um pagamento específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do pagamento a ser obtido.</param>
        /// <returns>Os detalhes do pagamento solicitado.</returns>
        /// <response code="200">Retorna o pagamento solicitado.</response>
        /// <response code="404">Pagamento não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Pagamento), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Pagamento>> GetPagamento(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);

            if (pagamento == null)
            {
                return NotFound();
            }

            return pagamento;
        }

        /// <summary>
        /// Atualiza um pagamento existente.
        /// </summary>
        /// <param name="id">O ID do pagamento a ser atualizado.</param>
        /// <param name="pagamento">Os dados atualizados do pagamento.</param>
        /// <returns>Uma resposta sem conteúdo se bem-sucedido.</returns>
        /// <response code="204">Pagamento atualizado com sucesso.</response>
        /// <response code="400">ID fornecido é inconsistente com o ID do pagamento.</response>
        /// <response code="404">Pagamento não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutPagamento(int id, Pagamento pagamento)
        {
            if (id != pagamento.PagamentoId)
            {
                return BadRequest();
            }

            _context.Entry(pagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Adiciona um novo pagamento.
        /// </summary>
        /// <param name="pagamento">Os dados do novo pagamento.</param>
        /// <returns>Os detalhes do pagamento criado.</returns>
        /// <response code="201">Pagamento criado com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Pagamento), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Pagamento>> PostPagamento(Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPagamento", new { id = pagamento.PagamentoId }, pagamento);
        }

        /// <summary>
        /// Exclui um pagamento existente.
        /// </summary>
        /// <param name="id">O ID do pagamento a ser excluído.</param>
        /// <returns>Uma resposta sem conteúdo se bem-sucedido.</returns>
        /// <response code="204">Pagamento excluído com sucesso.</response>
        /// <response code="404">Pagamento não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeletePagamento(int id)
        {
            var pagamento = await _context.Pagamentos.FindAsync(id);
            if (pagamento == null)
            {
                return NotFound();
            }

            _context.Pagamentos.Remove(pagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagamentoExists(int id)
        {
            return _context.Pagamentos.Any(e => e.PagamentoId == id);
        }
    }
}


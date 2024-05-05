using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraDeVeiculosApi.Models;
using LocadoraDeVeiculosApi.Data;
using System.Net;

namespace LocadoraDeVeiculosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly LocadoraContext _context;

        public ReservasController(LocadoraContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma lista de todas as reservas.
        /// </summary>
        /// <returns>Uma lista de reservas.</returns>
        /// <response code="200">Retorna a lista de reservas.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Reserva>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _context.Reservas.ToListAsync();
        }

        /// <summary>
        /// Obtém uma reserva específica pelo ID.
        /// </summary>
        /// <param name="id">O ID da reserva a ser obtida.</param>
        /// <returns>Os detalhes da reserva solicitada.</returns>
        /// <response code="200">Retorna a reserva solicitada.</response>
        /// <response code="404">Reserva não encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Reserva), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        /// <summary>
        /// Atualiza uma reserva existente.
        /// </summary>
        /// <param name="id">O ID da reserva a ser atualizada.</param>
        /// <param name="reserva">Os dados atualizados da reserva.</param>
        /// <returns>Uma resposta sem conteúdo se bem-sucedido.</returns>
        /// <response code="204">Reserva atualizada com sucesso.</response>
        /// <response code="400">ID fornecido é inconsistente com o ID da reserva.</response>
        /// <response code="404">Reserva não encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {
            if (id != reserva.ReservaId)
            {
                return BadRequest();
            }

            _context.Entry(reserva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
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
        /// Adiciona uma nova reserva.
        /// </summary>
        /// <param name="reserva">Os dados da nova reserva.</param>
        /// <returns>Os detalhes da reserva criada.</returns>
        /// <response code="201">Reserva criada com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Reserva), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserva", new { id = reserva.ReservaId }, reserva);
        }

        /// <summary>
        /// Exclui uma reserva existente.
        /// </summary>
        /// <param name="id">O ID da reserva a ser excluída.</param>
        /// <returns>Uma resposta sem conteúdo se bem-sucedido.</returns>
        /// <response code="204">Reserva excluída com sucesso.</response>
        /// <response code="404">Reserva não encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.ReservaId == id);
        }
    }
}


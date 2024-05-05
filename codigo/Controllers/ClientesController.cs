using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraDeVeiculosApi.Models;
using LocadoraDeVeiculosApi.Data;
using System.Net;

namespace LocadoraDeVeiculosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly LocadoraContext _context;

        public ClientesController(LocadoraContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os clientes.
        /// </summary>
        /// <returns>Uma lista de clientes.</returns>
        /// <response code="200">Retorna a lista de clientes.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Cliente>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        /// <summary>
        /// Obtém um cliente específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do cliente a ser obtido.</param>
        /// <returns>Os detalhes do cliente solicitado.</returns>
        /// <response code="200">Retorna o cliente solicitado.</response>
        /// <response code="404">Cliente não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cliente), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="cliente">Os dados atualizados do cliente.</param>
        /// <returns>Uma resposta sem conteúdo se bem-sucedido.</returns>
        /// <response code="204">Cliente atualizado com sucesso.</response>
        /// <response code="400">ID fornecido é inconsistente com o ID do cliente.</response>
        /// <response code="404">Cliente não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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
        /// Adiciona um novo cliente.
        /// </summary>
        /// <param name="cliente">Os dados do novo cliente.</param>
        /// <returns>Os detalhes do cliente criado.</returns>
        /// <response code="201">Cliente criado com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Cliente), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.ClienteId }, cliente);
        }

        /// <summary>
        /// Exclui um cliente existente.
        /// </summary>
        /// <param name="id">O ID do cliente a ser excluído.</param>
        /// <returns>Uma resposta sem conteúdo se bem-sucedido.</returns>
        /// <response code="204">Cliente excluído com sucesso.</response>
        /// <response code="404">Cliente não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }
    }
}



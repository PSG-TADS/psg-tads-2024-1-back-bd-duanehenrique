using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraDeVeiculosApi.Models;
using LocadoraDeVeiculosApi.Data;
using System.Net;

namespace LocadoraDeVeiculosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly LocadoraContext _context;

        public VeiculoController(LocadoraContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os veículos.
        /// </summary>
        /// <returns>Uma lista de veículos.</returns>
        /// <response code="200">Retorna a lista de veículos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Veiculo>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculos()
        {
            return await _context.Veiculos.ToListAsync();
        }

        /// <summary>
        /// Obtém um veículo específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do veículo a ser obtido.</param>
        /// <returns>Os detalhes do veículo solicitado.</returns>
        /// <response code="200">Retorna o veículo solicitado.</response>
        /// <response code="404">Veículo não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Veiculo), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Veiculo>> GetVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);

            if (veiculo == null)
            {
                return NotFound();
            }

            return veiculo;
        }

        /// <summary>
        /// Atualiza um veículo existente.
        /// </summary>
        /// <param name="id">O ID do veículo a ser atualizado.</param>
        /// <param name="veiculo">Os dados atualizados do veículo.</param>
        /// <returns>Uma resposta sem conteúdo se bem-sucedido.</returns>
        /// <response code="204">Veículo atualizado com sucesso.</response>
        /// <response code="400">ID fornecido é inconsistente com o ID do veículo.</response>
        /// <response code="404">Veículo não encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutVeiculo(int id, Veiculo veiculo)
        {
            if (id != veiculo.VeiculoId)
            {
                return BadRequest();
            }

            _context.Entry(veiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeiculoExists(id))
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
        /// Adiciona um novo veículo.
        /// </summary>
        /// <param name="veiculo">Os dados do novo veículo.</param>
        /// <returns>Os detalhes do veículo criado.</returns>
        /// <response code="201">Veículo criado com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Veiculo), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Veiculo>> PostVeiculo(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVeiculo", new { id = veiculo.VeiculoId }, veiculo);
        }

        /// <summary>
        /// Exclui um veículo existente.
        /// </summary>
        /// <param name="id">O ID do veículo a ser excluído.</param>
        /// <returns>Uma resposta sem conteúdo se bem-sucedido.</returns>
        /// <response code="204">Veículo excluído com sucesso.</response>
        /// <response code="404">Veículo não encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VeiculoExists(int id)
        {
            return _context.Veiculos.Any(e => e.VeiculoId == id);
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoraDeVeiculosApi.Models;
using LocadoraDeVeiculosApi.Data;
using System.Net;

namespace LocadoraDeVeiculosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaVeiculoController : ControllerBase
    {
        private readonly LocadoraContext _context;

        public CategoriaVeiculoController(LocadoraContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todas as categorias de veículos.
        /// </summary>
        /// <returns>Retorna todas as categorias de veículos disponíveis.</returns>
        /// <response code="200">Categorias de veículos retornadas com sucesso.</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CategoriaVeiculo>>> GetCategoriaVeiculos()
        {
            var categorias = await _context.CategoriaVeiculos.ToListAsync();
            return Ok(categorias);
        }


        /// <summary>
        /// Obtém uma categoria de veículo pelo ID.
        /// </summary>
        /// <param name="id">O ID da categoria de veículo.</param>
        /// <returns>Retorna a categoria de veículo solicitada ou um erro se não encontrada.</returns>
        /// <response code="200">Categoria de veículo retornada com sucesso.</response>
        /// <response code="404">Categoria de veículo não encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoriaVeiculo), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CategoriaVeiculo>> GetCategoriaVeiculo(int id)
        {
            var categoriaVeiculo = await _context.CategoriaVeiculos.FindAsync(id);
            if (categoriaVeiculo == null)
            {
                return NotFound();
            }

            return categoriaVeiculo;
        }

        /// <summary>
        /// Atualiza uma categoria de veículo.
        /// </summary>
        /// <param name="id">O ID da categoria para atualizar.</param>
        /// <param name="categoriaVeiculo">Dados atualizados da categoria de veículo.</param>
        /// <returns>Retorna NoContent se atualizado com sucesso, NotFound se a categoria não existir, ou BadRequest se o ID for inconsistente.</returns>
        /// <response code="204">Categoria de veículo atualizada com sucesso.</response>
        /// <response code="400">ID inconsistente.</response>
        /// <response code="404">Categoria de veículo não encontrada.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutCategoriaVeiculo(int id, CategoriaVeiculo categoriaVeiculo)
        {
            if (id != categoriaVeiculo.CategoriaVeiculoId)
            {
                return BadRequest();
            }

            _context.Entry(categoriaVeiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaVeiculoExists(id))
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
        /// Cria uma nova categoria de veículo.
        /// </summary>
        /// <param name="categoriaVeiculo">Dados da nova categoria de veículo.</param>
        /// <returns>Retorna a categoria de veículo criada.</returns>
        /// <response code="201">Categoria de veículo criada com sucesso.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CategoriaVeiculo), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<CategoriaVeiculo>> PostCategoriaVeiculo(CategoriaVeiculo categoriaVeiculo)
        {
            _context.CategoriaVeiculos.Add(categoriaVeiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaVeiculo", new { id = categoriaVeiculo.CategoriaVeiculoId }, categoriaVeiculo);
        }

        /// <summary>
        /// Exclui uma categoria de veículo.
        /// </summary>
        /// <param name="id">O ID da categoria de veículo a ser excluída.</param>
        /// <returns>Retorna NoContent se excluída com sucesso, ou NotFound se não encontrada.</returns>
        /// <response code="204">Categoria de veículo excluída com sucesso.</response>
        /// <response code="404">Categoria de veículo não encontrada.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCategoriaVeiculo(int id)
        {
            var categoriaVeiculo = await _context.CategoriaVeiculos.FindAsync(id);
            if (categoriaVeiculo == null)
            {
                return NotFound();
            }

            _context.CategoriaVeiculos.Remove(categoriaVeiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaVeiculoExists(int id)
        {
            return _context.CategoriaVeiculos.Any(e => e.CategoriaVeiculoId == id);
        }
    }
}

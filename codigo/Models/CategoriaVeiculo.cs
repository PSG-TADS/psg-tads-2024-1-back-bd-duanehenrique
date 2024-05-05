using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocadoraDeVeiculosApi.Models;


namespace LocadoraDeVeiculosApi.Models
{
    public class CategoriaVeiculo
    {
        public CategoriaVeiculo()
        {
            Nome = string.Empty; // valor padrão não-nulo
            Descricao = string.Empty; // valor padrão não-nulo
            Veiculos = new HashSet<Veiculo>(); // inicializa a coleção
        }

        [Key]
        public int CategoriaVeiculoId { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome da categoria não pode exceder 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(255, ErrorMessage = "A descrição não pode exceder 255 caracteres.")]
        public string Descricao { get; set; }

        // Relacionamento com Veiculos
        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }
}

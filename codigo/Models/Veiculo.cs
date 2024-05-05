using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocadoraDeVeiculosApi.Models
{
    public class Veiculo
    {
        public Veiculo()
        {
            Placa = string.Empty;
            Marca = string.Empty;
            Modelo = string.Empty;
            CategoriaVeiculo = new CategoriaVeiculo(); // se CategoriaVeiculo pode ser nulo, considere torná-la anulável ou remover essa linha se você estiver usando lazy loading
            Reservas = new HashSet<Reserva>();
        }

        private static readonly int CurrentYear = DateTime.Now.Year;  // Valor constante correto

        [Key]
        public int VeiculoId { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória.")]
        [RegularExpression(@"^[A-Z]{3}-\d{4}$", ErrorMessage = "A placa deve seguir o formato 'AAA-1234'.")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [StringLength(50, ErrorMessage = "A marca não pode exceder 50 caracteres.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [StringLength(50, ErrorMessage = "O modelo não pode exceder 50 caracteres.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Range(1900, 2023, ErrorMessage = "O ano do veículo é inválido.")]  // Corrigido para um valor constante
        public int Ano { get; set; }

        [Required(ErrorMessage = "O preço da diária é obrigatório.")]
        [Range(0.01, 9999.99, ErrorMessage = "O preço da diária deve ser positivo e razoável.")]
        [DataType(DataType.Currency)]
        public decimal PrecoDiaria { get; set; }

        [Required(ErrorMessage = "O ID da categoria do veículo é obrigatório.")]
        public int CategoriaVeiculoId { get; set; }

        [ForeignKey("CategoriaVeiculoId")]
        public virtual CategoriaVeiculo CategoriaVeiculo { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
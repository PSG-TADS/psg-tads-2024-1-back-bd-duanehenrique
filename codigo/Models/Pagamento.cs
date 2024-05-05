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
    public class Pagamento
    {
        public Pagamento()
        {
            Reserva = new Reserva();
            MetodoPagamento = string.Empty; // valor padrão não-nulo
        }

        [Key]
        public int PagamentoId { get; set; }

        [Required(ErrorMessage = "O valor do pagamento é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "A data do pagamento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataPagamento { get; set; }

        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        [StringLength(50, ErrorMessage = "O método de pagamento não pode exceder 50 caracteres.")]
        public string MetodoPagamento { get; set; }

        // Chave estrangeira para Reserva
        [Required(ErrorMessage = "O identificador da reserva é obrigatório.")]
        [ForeignKey("ReservaId")]
        public int ReservaId { get; set; }
        public virtual Reserva Reserva { get; set; }
    }
}

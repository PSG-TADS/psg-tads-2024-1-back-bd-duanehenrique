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
    public class Reserva
    {
        public Reserva()
        {
            Cliente = new Cliente();
            Veiculo = new Veiculo();  
            Pagamentos = new List<Pagamento>();  // Inicializa a coleção vazia
        }

        [Key]
        public int ReservaId { get; set; }

        [Required(ErrorMessage = "O identificador do veículo é obrigatório.")]
        public int VeiculoId { get; set; }

        [Required(ErrorMessage = "O identificador do cliente é obrigatório.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "A data de início da reserva é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim da reserva é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "O valor total é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal { get; set; }

        // Relacionamentos
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("VeiculoId")]
        public virtual Veiculo Veiculo { get; set; }

        public virtual ICollection<Pagamento> Pagamentos { get; set; }
    }

}

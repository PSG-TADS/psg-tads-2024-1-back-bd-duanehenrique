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
    public class Cliente
    {
        public Cliente()
        {
            Nome = string.Empty; // valor padrão não-nulo
            Email = string.Empty; // valor padrão não-nulo
            CPF = string.Empty; // valor padrão não-nulo
            Endereco = string.Empty; // valor padrão não-nulo
            Telefone = string.Empty; // valor padrão não-nulo
            Reservas = new HashSet<Reserva>(); // inicializa a coleção
        }

        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais que 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "Formato de CPF inválido.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [ValidarIdadeMinima(18, ErrorMessage = "O cliente deve ter pelo menos 18 anos.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "Formato de telefone inválido.")]
        public string Telefone { get; set; }

        // Relacionamento com Reservas
        public virtual ICollection<Reserva> Reservas { get; set; }
    }

    public class ValidarIdadeMinimaAttribute : ValidationAttribute
    {
        private readonly int _idadeMinima;

        public ValidarIdadeMinimaAttribute(int idadeMinima)
        {
            _idadeMinima = idadeMinima;
        }

        public override bool IsValid(object? value)  // Permite explicitamente valores nulos
        {
            // Verifica se o valor é nulo
            if (value is null)
            {
                return true; // Retorna true aqui porque [Required] é responsável por validar a nulidade
            }

            // Tentativa de converter o valor para DateTime de forma segura
            if (value is DateTime dataNascimento)
            {
                // Calcula a idade e verifica se é maior ou igual à idade mínima
                return (DateTime.Now.Year - dataNascimento.Year) >= _idadeMinima;
            }

            // Se a conversão falhar, retorna false, pois o tipo de dado é inesperado
            return false;
        }
    }
}

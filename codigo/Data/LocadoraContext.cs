using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocadoraDeVeiculosApi.Data;
using LocadoraDeVeiculosApi.Models;
using LocadoraDeVeiculosApi.Migrations;


namespace LocadoraDeVeiculosApi.Data
{
    public class LocadoraContext : DbContext
    {
        public LocadoraContext(DbContextOptions<LocadoraContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<CategoriaVeiculo> CategoriaVeiculos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Garante que a placa do veículo é única
            modelBuilder.Entity<Veiculo>()
                .HasIndex(v => v.Placa)
                .IsUnique();

            // Configuração da relação Cliente-Reserva
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId);

            // Configuração da relação Veículo-Reserva
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Veiculo)
                .WithMany(v => v.Reservas)
                .HasForeignKey(r => r.VeiculoId);

            // Configuração da relação Reserva-Pagamento
            modelBuilder.Entity<Pagamento>()
                .HasOne(p => p.Reserva)
                .WithMany(r => r.Pagamentos)
                .HasForeignKey(p => p.ReservaId);

            // Definindo a precisão para propriedades decimais
            modelBuilder.Entity<Pagamento>()
                .Property(p => p.Valor)
                .HasPrecision(18, 2); // Adiciona precisão e escala para o campo 'Valor' em Pagamento

            modelBuilder.Entity<Reserva>()
                .Property(r => r.ValorTotal)
                .HasPrecision(18, 2); // Adiciona precisão e escala para o campo 'ValorTotal' em Reserva

            modelBuilder.Entity<Veiculo>()
                .Property(v => v.PrecoDiaria)
                .HasPrecision(18, 2); // Adiciona precisão e escala para o campo 'PrecoDiaria' em Veiculo

            base.OnModelCreating(modelBuilder);
        }
    }
}


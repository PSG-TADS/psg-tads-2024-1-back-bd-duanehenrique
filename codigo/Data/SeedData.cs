using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using LocadoraDeVeiculosApi.Data;
using LocadoraDeVeiculosApi.Models;
using LocadoraDeVeiculosApi.Migrations;


public static class SeedData
{
    public static void Initialize(LocadoraContext context)
    {
        // Verifica se já existem dados
        if (!context.CategoriaVeiculos.Any())
        {
            context.CategoriaVeiculos.AddRange(
                new CategoriaVeiculo { Nome = "Econômico", Descricao = "Veículos econômicos e compactos" },
                new CategoriaVeiculo { Nome = "Luxo", Descricao = "Veículos de luxo e espaçosos" }
            );
        }

        if (!context.Veiculos.Any())
        {
            context.Veiculos.AddRange(
                new Veiculo { Placa = "ABC-1234", Marca = "Volkswagen", Modelo = "Gol", Ano = 2020, PrecoDiaria = 99.99M },
                new Veiculo { Placa = "XYZ-9876", Marca = "Fiat", Modelo = "Uno", Ano = 2021, PrecoDiaria = 79.99M }
            );
        }

        if (!context.Clientes.Any())
        {
            context.Clientes.AddRange(
                new Cliente { Nome = "João Silva", Email = "joao.silva@example.com", CPF = "123.456.789-00", DataNascimento = new DateTime(1985, 5, 1), Endereco = "Rua Fictícia, 123", Telefone = "(31) 98765-4321" },
                new Cliente { Nome = "Maria Oliveira", Email = "maria.oliveira@example.com", CPF = "987.654.321-11", DataNascimento = new DateTime(1990, 7, 23), Endereco = "Av. Imaginária, 987", Telefone = "(31) 12345-6789" }
            );
        }

        if (!context.Pagamentos.Any())
        {
            context.Pagamentos.AddRange(
                new Pagamento { DataPagamento = DateTime.Now, Valor = 200.50M, MetodoPagamento = "Cartão de Crédito" },
                new Pagamento { DataPagamento = DateTime.Now.AddDays(-1), Valor = 150.75M, MetodoPagamento = "Dinheiro" }
            );
        }

        if (!context.Reservas.Any())
        {
            context.Reservas.AddRange(
                new Reserva { DataInicio = DateTime.Today.AddDays(1), DataFim = DateTime.Today.AddDays(3), ClienteId = 1, VeiculoId = 1 },
                new Reserva { DataInicio = DateTime.Today.AddDays(2), DataFim = DateTime.Today.AddDays(5), ClienteId = 1, VeiculoId = 1 }
            );
        }

        context.SaveChanges();
    }

    public static void ResetAndSeedDatabase(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<LocadoraContext>();

            try
            {
                // Limpa as tabelas
                context.Database.ExecuteSqlRaw("DELETE FROM CategoriaVeiculos");
                context.Database.ExecuteSqlRaw("DELETE FROM Veiculos");
                context.Database.ExecuteSqlRaw("DELETE FROM Clientes");
                context.Database.ExecuteSqlRaw("DELETE FROM Pagamentos");
                context.Database.ExecuteSqlRaw("DELETE FROM Reservas");

                context.SaveChanges();

                // Inicializa com novos dados
                Initialize(context);
            }
            catch (Exception ex)
            {
                // Log do erro
                Console.WriteLine("Erro ao reiniciar e popular o banco de dados: " + ex.Message);
            }
        }
    }
}


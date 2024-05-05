using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using LocadoraDeVeiculosApi.Data;
using LocadoraDeVeiculosApi.Models;
using LocadoraDeVeiculosApi.Migrations;


namespace LocadoraDeVeiculosApi.Data
{
    public class LocadoraContextFactory : IDesignTimeDbContextFactory<LocadoraContext>
    {
        public LocadoraContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<LocadoraContext>();
            var connectionString = configuration.GetConnectionString("LocadoraDeVeiculosConnection");
            builder.UseSqlServer(connectionString);
            return new LocadoraContext(builder.Options);
        }
    }
}

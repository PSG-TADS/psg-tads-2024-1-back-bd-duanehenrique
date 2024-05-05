using Microsoft.OpenApi.Models;
using System.Reflection;
using LocadoraDeVeiculosApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionando o contexto do banco de dados com a string de conexão
builder.Services.AddDbContext<LocadoraContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocadoraDeVeiculosConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();  // Adiciona suporte para controllers

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Locadora De Veiculos API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        // Chamada ao método para resetar e repopular o banco de dados
        SeedData.ResetAndSeedDatabase(app.Services);
    }

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Locadora De Veiculos API V1");
        c.RoutePrefix = "swagger";
    });
}

// Endpoint para redirecionar para o Swagger UI
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();  // Mapeia os controllers

app.Run();



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
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Locadora De Veiculos API V1");
        c.RoutePrefix = "swagger";
    });
}

// Adiciona um escopo para inicialização de dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<LocadoraContext>();
        // Inicializa o banco de dados com Seed Data
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao popular o banco de dados.");
    }
}

// Endpoint para redirecionar para o Swagger UI
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();  // Mapeia os controllers

app.Run();



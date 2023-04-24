using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Configuration;
using webapi;
using webapi.Util;

var builder = WebApplication.CreateBuilder(args);

// Adiciona a string de conexão ao serviço Singleton
builder.Services.AddSingleton<ConectaBanco>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
    return new ConectaBanco(connectionString);
});

// Configura a injeção de dependência para ClienteDAO
builder.Services.AddScoped<ClienteDAO>();

// Configura a injeção de dependência para MySqlConnection
builder.Services.AddScoped<MySqlConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
    return new MySqlConnection(connectionString);
});

builder.Services.AddControllers();

// Configura o Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Adiciona o uso do Swagger ao pipeline de requisição HTTP
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder =>
{
    builder.WithOrigins("https://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

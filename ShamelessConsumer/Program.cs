using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MessagesStorage>();
builder.Services.AddSingleton<ShamelessConsumerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Inicializa o consumidor
var consumerService = app.Services.GetRequiredService<ShamelessConsumerService>();
consumerService.StartListening();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/mensagens", (MessagesStorage storage) =>
{
    return Results.Ok(storage.GetAll());
});

app.Run();

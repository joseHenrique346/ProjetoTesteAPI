using ProjetoTesteAPI.Extensions;
using ProjetoTesteAPI.Infrastructure;
using ProjetoTesteAPI.Infrastructure.Interfaces;
using ProjetoTesteAPI.Infrastructure.Repositories;
using ProjetoTesteAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureContext(builder.Configuration);
builder.Services.ConfigureAddition();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

var app = builder.Build();

app.ApplySwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
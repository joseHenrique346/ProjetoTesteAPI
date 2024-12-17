using ProjetoTesteAPI.Extensions;
using ProjetoTesteAPI.Infrastructure;
using ProjetoTesteAPI.Infrastructure.Interfaces;
using ProjetoTesteAPI.Infrastructure.Repositories;
using ProjetoTesteAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureContext(builder.Configuration);

builder.Services.AddScoped<IRepository<Brand>, BrandRepository>();
builder.Services.AddScoped<IRepository<Product>,  ProductRepository>();
builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.ApplySwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
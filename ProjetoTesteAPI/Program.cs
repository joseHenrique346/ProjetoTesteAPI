using Microsoft.AspNetCore.Identity;
using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Extensions;
using ProjetoTesteAPI.Models;
using System.Text.Json.Serialization;
using static ProjetoTesteAPI.Context.AppDbContext;
using ProjetoTesteAPI.Filters;  

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureContext(builder.Configuration);
builder.Services.ConfigureAddition();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new GlobalExceptionFilter());  
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.AddJWTAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

app.ApplySwagger();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await UserSeed.SeedAdminUserAsync(context);
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Identity;
using ProjetoTesteAPI.Context;
using ProjetoTesteAPI.Extensions;
using ProjetoTesteAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureContext(builder.Configuration);
builder.Services.ConfigureAddition();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.AddJWTAuthentication(builder.Configuration); 

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    InitializeAdminUser(dbContext);
}

app.ApplySwagger();

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapControllers();

app.Run();

void InitializeAdminUser(AppDbContext dbContext)
{
    if (!dbContext.Set<Client>().Any(c => c.Role == "admin"))
    {
        var passwordHasher = new PasswordHasher<Client>();
        var admin = new Client
        {
            Name = "admin",
            Email = "admin@admin.com",
            CPF = "000.000.000-00",
            Phone = "000000000",
            Role = "admin",
            PasswordHash = passwordHasher.HashPassword(null, "admin") 
        };

        dbContext.Set<Client>().Add(admin);
        dbContext.SaveChanges();
    }
}
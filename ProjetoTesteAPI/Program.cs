using ProjetoTesteAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureContext(builder.Configuration);
builder.Services.ConfigureAddition();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.AddJWTAuthentication(builder.Configuration); 

builder.Services.AddAuthorization();

var app = builder.Build();

app.ApplySwagger();

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapControllers();

app.Run();

using System.Text;
using GestionClientes.API.Data;
using GestionClientes.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var connectionString = config.GetConnectionString("GestionClientesDb") ?? throw new InvalidOperationException("Connection string 'GestionClientesDb' not found.");

//Bd
builder.Services.AddDbContext<GestionClientesDbContext>(options =>
    options.UseSqlServer(connectionString));
//Servicios
builder.Services.AddScoped<ISocioService, SocioService>();
builder.Services.AddScoped<IEntrenadorService, EntrenadorService>();
builder.Services.AddScoped<IMembresiaService, MembresiaService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// JWT 
var key = Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;    
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = config["Jwt:Issuer"],

            ValidateAudience = true,
            ValidAudience = config["Jwt:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            // ESTO ES CLAVE: Mapea los nombres de claims cortos a las propiedades de User
            NameClaimType = "sub",  // Ahora User.Identity.Name leerá el ID del 'sub'
            RoleClaimType = "role"  // Ahora [Authorize(Roles="...")] leerá del claim 'role'            
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

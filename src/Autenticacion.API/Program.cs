using System.Text;
using Autenticacion.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var connectionString = config.GetConnectionString("AutenticacionDb");

//BD
builder.Services.AddDbContext<AutenticacionDbContext>(options =>
    options.UseSqlServer(connectionString));

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

builder.Services.AddControllers();
builder.Services.AddOpenApi();

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

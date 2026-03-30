using System;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text;
using Autenticacion.API.Models.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Autenticacion.API.Services;

public class AuthService(IConfiguration config)
{
    private readonly IConfiguration _config = config;

    public string GenerateJwtToken(Usuario usuario)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Usamos un Dictionary para los claims, es más eficiente con JsonWebTokenHandler
        var claims = new Dictionary<string, object>
        {
            // 'sub' es el estándar moderno para el ID de usuario
            [JwtRegisteredClaimNames.Sub] = usuario.Id.ToString(),
            [JwtRegisteredClaimNames.Email] = usuario.Correo,
            [JwtRegisteredClaimNames.Name] = usuario.Nombre,
            [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
        };

        // Agregar roles usando la clave estándar de roles de JWT
        var roles = usuario.UsuarioRoles.Select(r => r.Rol.Nombre).ToArray();
        claims.Add("role", roles); 

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            Claims = claims,
            Expires = DateTime.UtcNow.AddHours(double.Parse(_config["Jwt:ExpirationHours"]!)),
            SigningCredentials = credentials
        };

        var handler = new JsonWebTokenHandler();
        
        // Genera el token directamente como string (más rápido)
        return handler.CreateToken(tokenDescriptor);
    }
}
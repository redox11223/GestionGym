using System.ComponentModel.DataAnnotations;

namespace Autenticacion.API.Models.Dtos;

public record class LoginDto(
    [Required(ErrorMessage = "Credenciales inválidas.")]
    [EmailAddress(ErrorMessage = "Credenciales inválidas.")]
    string Correo,
    
    [Required(ErrorMessage = "Credenciales inválidas.")]
    [MinLength(8, ErrorMessage = "Credenciales inválidas.")]
    string Contrasena
)
{

}

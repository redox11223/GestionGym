using System.ComponentModel.DataAnnotations;

namespace Autenticacion.API.Models.Dtos;

public record class LoginDto(
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo no es válido.")]
    string Correo,
    
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    string Contrasena
)
{

}

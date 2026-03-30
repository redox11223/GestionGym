using System.ComponentModel.DataAnnotations;

namespace Autenticacion.API.Models.Dtos;

public record class CreateUsuarioDto(
    [Required( ErrorMessage = "El nombre es obligatorio.")]
    [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y no puede estar vacío o solo espacios")]
    string Nombre,

    [Required( ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo no es válido.")]
    string Correo,

    [Required( ErrorMessage = "La contraseña es obligatoria.")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    string Contrasena,
 
    [Required( ErrorMessage = "El teléfono es obligatorio.")]
    [RegularExpression(@"^9\d{8}$", ErrorMessage = "Debe ser un celular peruano válido (9 dígitos).")]
    string Telefono,

    [Required( ErrorMessage = "Los roles son obligatorios.")]
    [MinLength(1, ErrorMessage = "Debe asignar al menos un rol.")]
    IEnumerable<Guid> Roles
)
{ }


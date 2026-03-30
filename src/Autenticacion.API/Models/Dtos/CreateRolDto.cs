using System.ComponentModel.DataAnnotations;

namespace Autenticacion.API.Models.Dtos;

public record class CreateRolDto(
    [Required( ErrorMessage = "El nombre es obligatorio.")]
    [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y no puede estar vacío o solo espacios")]
    string Nombre
)
{

}

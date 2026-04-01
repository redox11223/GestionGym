using System.ComponentModel.DataAnnotations;

namespace Autenticacion.API.Models.Dtos;

public record class CreateSocioDto(
    Guid UsuarioId,

    [Required(ErrorMessage = "El campo FechaNacimiento es obligatorio.")]
    DateOnly FechaNacimiento,

    [Required(ErrorMessage = "El campo Genero es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El género no puede exceder los 50 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El género solo puede contener letras y no puede estar vacío o solo espacios")]
    string Genero,

    [Required(ErrorMessage = "El campo AlturaCm es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "La altura debe ser un número positivo.")]
    decimal AlturaCm,

    [Required(ErrorMessage = "El campo PesoKg es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El peso debe ser un número positivo.")]
    decimal PesoKg,

    [Required(ErrorMessage = "El campo Nombre de emergencia es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre de emergencia no puede exceder los 100 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre de emergencia solo puede contener letras y no puede estar vacío o solo espacios")]
    string EmergenciaNombre,

    [Required(ErrorMessage = "El campo Telefono de emergencia es obligatorio.")]
    [Phone(ErrorMessage = "El teléfono de emergencia no tiene un formato válido.")]
    string EmergenciaTelefono,

    [Required(ErrorMessage = "El campo FechaRegistro es obligatorio.")]
    DateOnly FechaRegistro,

    [Required(ErrorMessage = "El campo EstaActivo es obligatorio.")]
    bool EstaActivo
)
{

}

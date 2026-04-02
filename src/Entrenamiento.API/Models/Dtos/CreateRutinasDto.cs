using System.ComponentModel.DataAnnotations;
namespace Entrenamiento.API.Models.Dtos;

public record class CreateRutinasDto 
(
    [Required(ErrorMessage = "El ID del socio es obligatorio.")]
    Guid SocioId,

    [Required(ErrorMessage = "El ID del entrenador es obligatorio.")]
    Guid EntrenadorId,

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y no puede estar vacío o solo espacios")]
    string Nombre,

    [MaxLength(200, ErrorMessage = "El objetivo no puede exceder los 200 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El objetivo solo puede contener letras y no puede estar vacío o solo espacios")]
    string ? Objetivo,

    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
    DateTime FechaInicio,

    [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
    DateTime FechaFin,

    [Required(ErrorMessage = "Debe incluir al menos un ejercicio.")]
    [MinLength(1, ErrorMessage = "Debe incluir al menos un ejercicio.")]
    IEnumerable<Guid> IdsEjercicios

): IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FechaFin < FechaInicio)
        {
            yield return new ValidationResult("La fecha de fin no puede ser anterior a la fecha de inicio.", [nameof(FechaFin)]);
        }
    }
}
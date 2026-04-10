using System.ComponentModel.DataAnnotations;

namespace GestionClientes.API.Models.Dtos;

public record class ComprarMembresiaDto(
    [Required(ErrorMessage = "El campo SocioId es obligatorio.")]
    Guid MembresiaId,

    [Required(ErrorMessage = "El campo MembresiaId es obligatorio.")]
    DateOnly FechaInicio,

    string? Notas
) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FechaInicio < DateOnly.FromDateTime(DateTime.Now))
        {
            yield return new ValidationResult("La fecha de inicio no puede ser en el pasado.", new[] { nameof(FechaInicio) });
        }
    }
}

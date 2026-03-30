using System.ComponentModel.DataAnnotations;
namespace Entrenamiento.API.Models.Dtos;

public record class CreateRutinasDto
(
    
    [Required(ErrorMessage = "El ID del socio es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del socio debe ser un número positivo.")]
    int SocioId,
    [Required(ErrorMessage = "El ID del entrenador es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del entrenador debe ser un número positivo.")]
    int EntrenadorId,

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y no puede estar vacío o solo espacios")]
    string Nombre,
    
    [Required(ErrorMessage = "El campo Objetivo es obligatorio.")] 
    [MaxLength(200, ErrorMessage = "El objetivo no puede exceder los 200 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El objetivo solo puede contener letras y no puede estar vacío o solo espacios")]
    string? Objetivo,

    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
    DateTime FechaInicio,
    
    [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
    DateTime FechaFin,
    
    [Required(ErrorMessage = "El estado Activo es obligatorio.")]
    bool Activa
);

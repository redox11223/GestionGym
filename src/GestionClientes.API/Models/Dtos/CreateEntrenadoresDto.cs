using System.ComponentModel.DataAnnotations;
namespace GestionClientes.API.Models.Dtos;

public record class CreateEntrenadoresDto
(
    [Required(ErrorMessage = "la especialidad es obligatorio.")]
    [MaxLength(100, ErrorMessage = "La especialidad no puede exceder los 100 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "La especialidad solo puede contener letras y no puede estar vacía o solo contener espacios")]
    string Especialidad,

    [Required(ErrorMessage = "Las certificaciones son obligatorias.")]
    [MaxLength(200, ErrorMessage = "Las certificaciones no pueden exceder los 200 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Las certificaciones solo pueden contener letras y no pueden estar vacías o solo contener espacios")]
    string Certificaciones,
    
    [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
    DateOnly FechaIngreso
    
);
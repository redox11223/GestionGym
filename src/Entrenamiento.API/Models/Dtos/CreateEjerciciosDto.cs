using System.ComponentModel.DataAnnotations;
namespace Entrenamiento.API.Models.Dtos;

public record class CreateEjerciciosDto(

    [Required(ErrorMessage = "El campo Nombre es obligatorio.")] 
    [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y no puede estar vacío o solo espacios")]
    string Nombre,

    [Required(ErrorMessage = "El campo Descripcion es obligatorio.")]
    [MaxLength(200, ErrorMessage = "La descripción no puede exceder los 200 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "La Descripcion solo puede contener letras y no puede estar vacío o solo espacios")]
    string Descripcion,

    [Required(ErrorMessage = "El campo GrupoMuscular es obligatorio.")] 
    [MaxLength(100, ErrorMessage = "El grupo muscular no puede exceder los 100 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El GrupoMuscular solo puede contener letras y no puede estar vacío o solo espacios")]
    string GrupoMuscular,

    [Required(ErrorMessage = "El estado de activación es obligatorio.")]
    bool EstaActivo
);

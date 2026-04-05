using System.ComponentModel.DataAnnotations;

namespace GestionClientes.API.Models.Dtos;

public record class CreateSocioDto
(
    [Required(ErrorMessage = "El campo FechaNacimiento es obligatorio.")]
    DateOnly FechaNacimiento,

    [Required(ErrorMessage = "El campo Genero es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El género no puede exceder los 50 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El género solo puede contener letras y no puede estar vacío o solo espacios")]
    string Genero,

    [Range(50, 250, ErrorMessage = "La altura debe estar entre 50 y 250 cm.")]
    double? AlturaCm,

    [Range(20, 300, ErrorMessage = "El peso debe estar entre 20 y 300 kg.")]
    double? PesoKg,

    [MaxLength(100, ErrorMessage = "El nombre de emergencia no puede exceder los 100 caracteres.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$", ErrorMessage = "El nombre de emergencia solo puede contener letras")]
    string? EmergenciaNombre,

    [Phone(ErrorMessage = "El teléfono de emergencia no tiene un formato válido.")]
    string? EmergenciaTelefono

);
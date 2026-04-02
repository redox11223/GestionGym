using System.ComponentModel.DataAnnotations;

namespace GestionClientes.API.Models.Dtos;

public record class CreateMembresiasDto
(
    
    [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    string  Nombre,

    [Required(ErrorMessage = "El campo Descripcion es obligatorio.")]
    [MaxLength(300, ErrorMessage = "La descripción no puede exceder los 300 caracteres.")]  
    string  Descripcion,

    [Required(ErrorMessage = "El campo DuracionDias es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "La duración en días debe ser un número positivo.")]
    int DuracionDias,

    [Required(ErrorMessage = "El campo Precio es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un número positivo.")]
    decimal Precio,

    [Required(ErrorMessage = "El campo 'EsRenovable' es obligatorio.")]
    bool EsRenovable,
    
    [Required(ErrorMessage = "El campo 'EstaActivo' es obligatorio.")]
    bool EstaActivo
);
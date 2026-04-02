using System.ComponentModel.DataAnnotations;

namespace GestionClientes.API.Models.Dtos;
public record class CreateAsistenciasDto
(
    [Required(ErrorMessage = "El ID del socio es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del socio debe ser un número positivo.")]
    int SocioId,

    [Required(ErrorMessage = "La fecha y hora de entrada es obligatoria.")]
    DateTime FechaHoraEntrada,
    
    DateTime? FechaHoraSalida,
    
    string? Observaciones,
    
    [Required(ErrorMessage = "El ID del usuario que registra la asistencia es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del usuario debe ser un número positivo.")]
    int RegistradaPorUsuarioId
);
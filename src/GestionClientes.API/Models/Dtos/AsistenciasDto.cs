

namespace GestionClientes.API.Models.Dtos;

public record class AsistenciasDto
(
    int Id,
    int SocioId,
    DateTime FechaHoraEntrada,
    DateTime? FechaHoraSalida,
    string? Observaciones,
    int RegistradaPorUsuarioId
);

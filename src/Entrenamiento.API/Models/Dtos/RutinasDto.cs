namespace Entrenamiento.API.Models.Dtos;

public record class RutinasDto
(
    Guid Id,
    int SocioId,
    int EntrenadorId,
    string Nombre,
    string? Objetivo,
    DateTime FechaInicio,
    DateTime FechaFin,
    bool Activa
);
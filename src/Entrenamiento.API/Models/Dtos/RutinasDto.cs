namespace Entrenamiento.API.Models.Dtos;

public record class RutinasDto
(
    Guid Id,
    Guid SocioId,
    Guid EntrenadorId,
    string Nombre,
    string? Objetivo,
    DateTime FechaInicio,
    DateTime FechaFin,
    bool Activa,
    IEnumerable<EjerciciosDto> Ejercicios
);
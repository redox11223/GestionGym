namespace Autenticacion.API.Models.Dtos;

public record class SocioDto(
    Guid Id,
    Guid UsuarioId,
    DateOnly FechaNacimiento,
    string Genero,
    double? AlturaCm,
    double? PesoKg,
    string? EmergenciaNombre,
    string? EmergenciaTelefono,
    DateTimeOffset FechaRegistro
)
{
    
}

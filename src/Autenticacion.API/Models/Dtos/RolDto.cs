namespace Autenticacion.API.Models.Dtos;

public record class RolDto(
    Guid Id,
    string Nombre,
    string NombreNormalizado
    )
{}

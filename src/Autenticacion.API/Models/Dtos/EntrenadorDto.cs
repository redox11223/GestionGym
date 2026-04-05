namespace Autenticacion.API.Models.Dtos;

public record class EntrenadorDto(
    Guid Id,
    Guid UsuarioId,
    string Especialidad,
    string Certificaciones,
    DateOnly? FechaIngreso,
    bool EstaActivo
)
{

}

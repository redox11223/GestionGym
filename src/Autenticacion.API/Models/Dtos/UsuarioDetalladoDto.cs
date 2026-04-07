namespace Autenticacion.API.Models.Dtos;

public record class UsuarioDetalladoDto(
    Guid Id,
    string Nombre,
    string Correo,
    string Telefono,
    IEnumerable<string> Roles,
    SocioDto? Socio,
    EntrenadorDto? Entrenador
)
{

}

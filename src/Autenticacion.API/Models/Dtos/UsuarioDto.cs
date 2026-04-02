namespace Autenticacion.API.Models.Dtos;

public record class UsuarioDto(
    Guid Id,
    string Nombre,
    string Correo,
    string Telefono,
    IEnumerable<string> Roles
)
{

}

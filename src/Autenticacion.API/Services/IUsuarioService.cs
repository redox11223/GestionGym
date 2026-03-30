using System;
using Autenticacion.API.Models.Dtos;

namespace Autenticacion.API.Services;

public interface IUsuarioService
{
    Task<UsuarioDto> UsuarioPorIdAsync(Guid id);
    Task<IEnumerable<UsuarioDto>> ObtenerUsuariosAsync();
    Task<bool> UsuarioExisteAsync(string correo);
    Task<UsuarioDto> CrearUsuarioAsync(CreateUsuarioDto createUsuarioDto);
}

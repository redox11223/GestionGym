using System;
using Autenticacion.API.Models.Dtos;

namespace Autenticacion.API.Services;

public interface IRolService
{
    Task<RolDto> RolPorIdAsync(Guid id);
    Task<RolDto> CrearRolAsync(CreateRolDto createRolDto);
    Task<IEnumerable<RolDto>> ObtenerRolesAsync();
    Task<RolDto> ActualizarRolAsync(Guid id, CreateRolDto updateRolDto);
    Task<IEnumerable<RolDto>> ObtenerRolesValidosAsync(IEnumerable<Guid> rolIds);
}

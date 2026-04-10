using System;
using GestionClientes.API.Models.Dtos;

namespace GestionClientes.API.Services;

public interface ISocioService
{
    Task<SocioDto> CrearSocioAsync(CreateSocioDto createSocioDto);
    Task<SocioDto> ObtenerSocioByIdAsync(Guid id);
    Task<IEnumerable<SocioDto>> ObtenerTodosSociosAsync();
    Task<SocioDto> ActualizarSocioAsync(Guid id, CreateSocioDto updateSocioDto);
    Task EliminarSocioAsync(Guid id);
    Task<SocioDto> UpsertSocioAsync(Guid id, CreateSocioDto createSocioDto);
    Task<bool> AsignarEntrenadorASocioAsync(Guid socioId, Guid entrenadorId);
    Task<bool> AsignarMembresiaASocioAsync(Guid socioId,ComprarMembresiaDto comprarMembresiaDto);
    Task<bool> ExisteSocioAsync(Guid id);  
}

using System;
using GestionClientes.API.Models.Dtos;

namespace GestionClientes.API.Services;

public interface IEntrenadorService
{
    Task<EntrenadoresDto> CrearEntrenadorAsync(CreateEntrenadoresDto createEntrenadorDto);
    Task<EntrenadoresDto> ObtenerEntrenadorByIdAsync(Guid id);
    Task<IEnumerable<EntrenadoresDto>> ObtenerEntrenadoresAsync();
    Task<EntrenadoresDto> ActualizarEntrenadorAsync(Guid id, CreateEntrenadoresDto updateEntrenadorDto);
    Task<bool> EliminarEntrenadorAsync(Guid id);
    Task<EntrenadoresDto> UpsertEntrenadorAsync(Guid id, CreateEntrenadoresDto updateEntrenadorDto);
}

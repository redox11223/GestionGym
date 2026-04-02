using System;
using GestionClientes.API.Models.Dtos;

namespace GestionClientes.API.Services;

public interface IMembresiaService
{
   Task<MembresiasDto> CrearMembresiaAsync(CreateMembresiasDto membresia);
    Task<MembresiasDto> ObtenerMembresiaByIdAsync(Guid id);
    Task<IEnumerable<MembresiasDto>> ObtenerMembresiasAsync();
    Task<MembresiasDto> ActualizarMembresiaAsync(Guid id, CreateMembresiasDto membresia);
    Task<bool> EliminarMembresiaAsync(Guid id);  
}

using System;
using Entrenamiento.API.Models.Dtos;

namespace Entrenamiento.API.Services;

public interface IRutinaService
{
    Task<IEnumerable<RutinasDto>> ObtenerRutinasAsync();
    Task<RutinasDto> ObtenerRutinaByIdAsync(Guid id);
    Task<RutinasDto> CrearRutinaAsync(CreateRutinasDto createRutinasDto);
    Task<RutinasDto> ActualizarRutinaAsync(Guid id, CreateRutinasDto updateRutinasDto);
    Task<bool> EliminarRutinaAsync(Guid id);
}

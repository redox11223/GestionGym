using System;
using Entrenamiento.API.Data;
using Entrenamiento.API.Models.Dtos;

namespace Entrenamiento.API.Services;

public class RutinaService : IRutinaService
{
    private readonly EntrenamientoDbContext _context;
    public RutinaService(EntrenamientoDbContext context)
    {
        _context = context;
    }
    public Task<RutinasDto> ActualizarRutinaAsync(Guid id, CreateRutinasDto updateRutinasDto)
    {
        throw new NotImplementedException();
    }

    public Task<RutinasDto> CrearRutinaAsync(CreateRutinasDto createRutinasDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EliminarRutinaAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RutinasDto> ObtenerRutinaByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RutinasDto>> ObtenerRutinasAsync()
    {
        throw new NotImplementedException();
    }
}

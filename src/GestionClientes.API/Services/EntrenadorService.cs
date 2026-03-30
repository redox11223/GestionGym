using System;
using GestionClientes.API.Data;
using GestionClientes.API.Models;
using GestionClientes.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GestionClientes.API.Services;

public class EntrenadorService : IEntrenadorService
{
    private readonly GestionClientesDbContext _context;
    public EntrenadorService(GestionClientesDbContext context)
    {
        _context = context;
    }
    public async Task<EntrenadoresDto> ActualizarEntrenadorAsync(Guid id, CreateEntrenadoresDto updateEntrenadorDto)
    {
        throw new NotImplementedException();
    }

    public async Task<EntrenadoresDto> CrearEntrenadorAsync(CreateEntrenadoresDto createEntrenadorDto)
    {
        var entrenador = new Entrenadores
        {
            UsuarioId = createEntrenadorDto.UsuarioId,
            Especialidad = createEntrenadorDto.Especialidad,
            Certificaciones = createEntrenadorDto.Certificaciones,
            FechaIngreso = createEntrenadorDto.FechaIngreso,
            EstaActivo = true
        };

        _context.Entrenadores.Add(entrenador);
        await _context.SaveChangesAsync();

        return MapToDto(entrenador);
    }

    public async Task<bool> EliminarEntrenadorAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<EntrenadoresDto> ObtenerEntrenadorByIdAsync(Guid id)
    {
        var entrenador = await _context.Entrenadores.FirstOrDefaultAsync(e => e.Id == id) ?? throw new KeyNotFoundException($"No se encontró el entrenador con id {id}");
        return MapToDto(entrenador);
    }

    public async Task<IEnumerable<EntrenadoresDto>> ObtenerEntrenadoresAsync()
    {
         return await _context.Entrenadores.Select(e=>new EntrenadoresDto
      (
        e.Id,
        e.UsuarioId,
        e.Especialidad,
        e.Certificaciones,
        e.FechaIngreso,
        e.EstaActivo
      )).ToListAsync();      
    }

    private static EntrenadoresDto MapToDto(Entrenadores entrenador)
    {
        return new EntrenadoresDto
        (   entrenador.Id,
            entrenador.UsuarioId,
            entrenador.Especialidad,
            entrenador.Certificaciones,
            entrenador.FechaIngreso,
            entrenador.EstaActivo
        );
    }
}

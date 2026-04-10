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

    public async Task<EntrenadoresDto> UpsertEntrenadorAsync(Guid id, CreateEntrenadoresDto createEntrenadorDto)
    {
        var entrenadorExistente = await _context.Entrenadores.FirstOrDefaultAsync(e => e.Id == id);
        var nombreNormalizado = createEntrenadorDto.Especialidad.Trim().ToLower();

        if (entrenadorExistente == null)
        {
            entrenadorExistente= new Entrenadores
            {
                Id = id,
                UsuarioId = id,
                Especialidad = nombreNormalizado,
                Certificaciones = createEntrenadorDto.Certificaciones,
            };

            _context.Entrenadores.Add(entrenadorExistente);

        }
        entrenadorExistente.Especialidad = nombreNormalizado;
        entrenadorExistente.Certificaciones = createEntrenadorDto.Certificaciones;
        entrenadorExistente.FechaIngreso = createEntrenadorDto.FechaIngreso;

        await _context.SaveChangesAsync();

        return MapToDto(entrenadorExistente);
    }
    public async Task<EntrenadoresDto> ActualizarEntrenadorAsync(Guid id, CreateEntrenadoresDto updateEntrenadorDto)
    {
        var nombreNormalizado = updateEntrenadorDto.Especialidad.Trim().ToLower();
        var entrenador = await _context.Entrenadores.FirstOrDefaultAsync(e => e.Id == id) ?? throw new KeyNotFoundException($"No se encontró el entrenador con id {id}");
        entrenador.UsuarioId = id;
        entrenador.Especialidad = nombreNormalizado;
        entrenador.Certificaciones = updateEntrenadorDto.Certificaciones;
        entrenador.FechaIngreso = updateEntrenadorDto.FechaIngreso;

        _context.Entrenadores.Update(entrenador);
        await _context.SaveChangesAsync();

        return MapToDto(entrenador);

    }

    public async Task<EntrenadoresDto> CrearEntrenadorAsync(CreateEntrenadoresDto createEntrenadorDto)
    {
        var nombreNormalizado = createEntrenadorDto.Especialidad.Trim().ToLower();
        var entrenador = new Entrenadores
        {
            Especialidad = nombreNormalizado,
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
        var entrenador = await _context.Entrenadores.FirstOrDefaultAsync(e => e.Id == id) ?? throw new KeyNotFoundException($"No se encontró el entrenador con id {id}");
        _context.Entrenadores.Remove(entrenador);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<EntrenadoresDto> ObtenerEntrenadorByIdAsync(Guid id)
    {
        var entrenador = await _context.Entrenadores.FirstOrDefaultAsync(e => e.Id == id) ?? throw new KeyNotFoundException($"No se encontró el entrenador con id {id}");
        return MapToDto(entrenador);
    }

    public async Task<IEnumerable<EntrenadoresDto>> ObtenerEntrenadoresAsync()
    {
        return await _context.Entrenadores.Select(e => new EntrenadoresDto
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
        (entrenador.Id,
            entrenador.UsuarioId,
            entrenador.Especialidad,
            entrenador.Certificaciones,
            entrenador.FechaIngreso,
            entrenador.EstaActivo
        );
    }
}

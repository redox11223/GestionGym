using System;
using GestionClientes.API.Data;
using GestionClientes.API.Models;
using GestionClientes.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GestionClientes.API.Services;

public class MembresiaService : IMembresiaService
{
    private readonly GestionClientesDbContext _context;

    public MembresiaService(GestionClientesDbContext context)
    {
        _context = context;
    }

    public async Task<MembresiasDto> ActualizarMembresiaAsync(Guid id, CreateMembresiasDto updateMembresiaDto)
    {
        var existingMembresia = await _context.Membresias.FirstOrDefaultAsync(m => m.Id == id) ?? throw new KeyNotFoundException($"No se encontró la membresía con id {id}");
        if (await _context.Membresias.AnyAsync(m => m.Nombre == updateMembresiaDto.Nombre && m.Id != id))
        {
            throw new InvalidOperationException("Ya existe una membresía con ese nombre.");
        }
        existingMembresia.Nombre = updateMembresiaDto.Nombre;
        existingMembresia.Descripcion = updateMembresiaDto.Descripcion;
        existingMembresia.DuracionDias = updateMembresiaDto.DuracionDias;
        existingMembresia.Precio = updateMembresiaDto.Precio;
        existingMembresia.EsRenovable = updateMembresiaDto.EsRenovable;
        existingMembresia.EstaActivo = updateMembresiaDto.EstaActivo;

        _context.Membresias.Update(existingMembresia);
        await _context.SaveChangesAsync();

        return MapToDto(existingMembresia);

    }

    public async Task<MembresiasDto> CrearMembresiaAsync(CreateMembresiasDto createMembresiaDto)
    {
        var membresia = new Membresias
        {
            Nombre = createMembresiaDto.Nombre,
            Descripcion = createMembresiaDto.Descripcion,
            DuracionDias = createMembresiaDto.DuracionDias,
            Precio = createMembresiaDto.Precio,
            EsRenovable = createMembresiaDto.EsRenovable,
            EstaActivo = true
        };

        _context.Membresias.Add(membresia);
        await _context.SaveChangesAsync();

        return MapToDto(membresia);
    }

    public async Task<bool> EliminarMembresiaAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<MembresiasDto> ObtenerMembresiaByIdAsync(Guid id)
    {
        var membresia = await _context.Membresias.FirstOrDefaultAsync(m => m.Id == id) ?? throw new KeyNotFoundException($"No se encontró la membresía con id {id}");
        return MapToDto(membresia);
    }

    public async Task<IEnumerable<MembresiasDto>> ObtenerMembresiasAsync()
    {
        return await _context.Membresias.Select(m => new MembresiasDto
        (
            m.Id,
            m.Nombre,
            m.Descripcion,
            m.DuracionDias,
            m.Precio,
            m.EsRenovable,
            m.EstaActivo
        )).ToListAsync();
    }

    private static MembresiasDto MapToDto(Membresias membresia)
    {
        return new MembresiasDto
        (
            membresia.Id,
            membresia.Nombre,
            membresia.Descripcion,
            membresia.DuracionDias,
            membresia.Precio,
            membresia.EsRenovable,
            membresia.EstaActivo
        );
    }

}


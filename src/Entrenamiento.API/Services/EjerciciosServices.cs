using System;
using Entrenamiento.API.Data;
using Entrenamiento.API.Models.Dtos;
using Entrenamiento.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entrenamiento.API.Services;

public class EjerciciosServices : IEjerciciosService
{
    private readonly EntrenamientoDbContext _context;
    public EjerciciosServices(EntrenamientoDbContext context)
    {
        _context = context;
    }

    public async Task<EjerciciosDto> AgregarEjercicios(CreateEjerciciosDto ejercicios)
    {
        if (await ExisteEjercicio(ejercicios.Nombre))
        {
            throw new InvalidOperationException("Ya existe un ejercicio con ese nombre.");
        }

        Ejercicios nuevoEjercicio = new()
        {
            Nombre = ejercicios.Nombre,
            Descripcion = ejercicios.Descripcion,
            GrupoMuscular = ejercicios.GrupoMuscular,
            EstaActivo = true
        };

        _context.Ejercicios.Add(nuevoEjercicio);
        await _context.SaveChangesAsync();
        return MapToDto(nuevoEjercicio);
    }

    public async Task<EjerciciosDto> ObtenerEjercicioPorId(Guid id)
    {
        var Ejercicio = await _context.Ejercicios.FirstOrDefaultAsync(e => e.Id == id) ?? throw new KeyNotFoundException("Este ejercicio no existe");
        return MapToDto(Ejercicio);
    }
    public async Task<IEnumerable<EjerciciosDto>> ObtenerTodosLosEjercicios()
    {
        return await _context.Ejercicios.Select(e => new EjerciciosDto(
            e.Id,
            e.Nombre,
            e.Descripcion,
            e.GrupoMuscular,
            e.EstaActivo
        )).ToListAsync();
    }
    public async Task<EjerciciosDto> ActualizarEjercicio(Guid id, CreateEjerciciosDto ejercicios)
    {
        var ExistingEjercicio = _context.Ejercicios.FirstOrDefault(e => e.Id == id) ?? throw new KeyNotFoundException("Esta ejercicio no existe"); ;
        if (await _context.Ejercicios.AnyAsync(e => e.Nombre == ejercicios.Nombre && e.Id != id))
        {
            throw new InvalidOperationException("Ya existe un ejercicio con ese nombre.");
        }
        ExistingEjercicio.Nombre = ejercicios.Nombre;
        ExistingEjercicio.Descripcion = ejercicios.Descripcion;
        ExistingEjercicio.GrupoMuscular = ejercicios.GrupoMuscular;
        ExistingEjercicio.EstaActivo = ejercicios.EstaActivo;
        _context.Ejercicios.Update(ExistingEjercicio);
        await _context.SaveChangesAsync();
        return MapToDto(ExistingEjercicio);
    }
    public async Task<bool> EliminarEjercicio(Guid id)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> ExisteEjercicio(string nombre)
    {
        return await _context.Ejercicios.AnyAsync(e => e.Nombre == nombre);
    }

    private static EjerciciosDto MapToDto(Ejercicios ejercicio)
    {
        return new EjerciciosDto(
            ejercicio.Id,
            ejercicio.Nombre,
            ejercicio.Descripcion,
            ejercicio.GrupoMuscular,
            ejercicio.EstaActivo
        );
    }
}

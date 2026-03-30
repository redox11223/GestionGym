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

    public async Task<Ejercicios> AgregarEjercicios(CreateEjerciciosDto ejercicios){
      var existingEjercicio = _context.Ejercicios.FirstOrDefault(e => e.Nombre == ejercicios.Nombre);
        if (existingEjercicio != null)
        {
            throw new Exception("Ya existe un ejercicio con ese nombre.");
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
        return nuevoEjercicio;
    }

    public async Task<Ejercicios> ObtenerEjercicioPorId(Guid id){
        var Ejercicio = await _context.Ejercicios.FirstOrDefaultAsync(e => e.Id == id)?? throw new KeyNotFoundException("Este ejercicio no existe");
        return new Ejercicios()
        {
            Id = Ejercicio.Id,
            Nombre = Ejercicio.Nombre,
            Descripcion = Ejercicio.Descripcion,
            GrupoMuscular = Ejercicio.GrupoMuscular,
            EstaActivo = Ejercicio.EstaActivo
        };
    }
    public async Task<IEnumerable<Ejercicios>> ObtenerTodosLosEjercicios(){
        return await _context.Ejercicios.ToListAsync();
    }
    public async Task<Ejercicios> ActualizarEjercicio(Guid id, CreateEjerciciosDto ejercicios){
        var ExistingEjercicio = _context.Ejercicios.FirstOrDefault(e => e.Id == id) ?? throw new KeyNotFoundException("Esta ejercicio no existe");;
        if(await _context.Ejercicios.AnyAsync(e => e.Nombre == ejercicios.Nombre && e.Id != id))
        {
            throw new InvalidOperationException("Ya existe un ejercicio con ese nombre.");
        }
        ExistingEjercicio.Nombre = ejercicios.Nombre;
        ExistingEjercicio.Descripcion = ejercicios.Descripcion;
        ExistingEjercicio.GrupoMuscular = ejercicios.GrupoMuscular;
        await _context.SaveChangesAsync();
        return ExistingEjercicio;
    }
   
    public async Task EliminarEjercicio(Guid id){

        var Ejercicio =  _context.Ejercicios.FirstOrDefault(e => e.Id == id) ?? throw new KeyNotFoundException("Este ejercicio no existe");
        _context.Ejercicios.Remove(Ejercicio);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> ExisteEjercicio(Guid id){
        return await _context.Ejercicios.AnyAsync(e => e.Id == id);
    
    }
}

using System;
using Entrenamiento.API.Data;
using Entrenamiento.API.Models.Dtos;
using Entrenamiento.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entrenamiento.API.Services;

public class RutinaService : IRutinaService
{
    private readonly EntrenamientoDbContext _context;
    private readonly IEjerciciosService _ejerciciosService;
    private readonly IGestionCliente _gestionCliente;
    public RutinaService(EntrenamientoDbContext context, IEjerciciosService ejerciciosService, IGestionCliente gestionCliente)
    {
        _context = context;
        _ejerciciosService = ejerciciosService;
        _gestionCliente = gestionCliente;
    }
    public Task<RutinasDto> ActualizarRutinaAsync(Guid id, CreateRutinasDto updateRutinasDto)
    {
        throw new NotImplementedException();
    }

    public async Task<RutinasDto> CrearRutinaAsync(CreateRutinasDto createRutinasDto)
    {
        var tareaSocio = _gestionCliente.ObtenerDatosSocio(createRutinasDto.SocioId);
        var tareaEntrenador = _gestionCliente.ObtenerDatosEntrenador(createRutinasDto.EntrenadorId);
        await Task.WhenAll(tareaSocio, tareaEntrenador);
        var ejerciciosValidos = await _ejerciciosService.ObtenerEjerciciosValidos(createRutinasDto.IdsEjercicios);
        if(ejerciciosValidos.Count() != createRutinasDto.IdsEjercicios.Count())
        {
            throw new InvalidOperationException("Algunos ejercicios no son válidos.");
        }
        var rutina = new Rutinas
        {
            SocioId = createRutinasDto.SocioId,
            EntrenadorId = createRutinasDto.EntrenadorId,
            Nombre = createRutinasDto.Nombre,
            Objetivo = createRutinasDto.Objetivo,
            FechaInicio = createRutinasDto.FechaInicio,
            FechaFin = createRutinasDto.FechaFin,
            Activa = true
        };
        rutina.RutinaEjercicios=ejerciciosValidos.Select(e => new RutinaEjercicios { 
            EjercicioId = e.Id,
            Orden = createRutinasDto.IdsEjercicios.ToList().IndexOf(e.Id)
            }).ToList();
        _context.Rutinas.Add(rutina);
        await _context.SaveChangesAsync();
        return MapToRutinasDto(rutina, ejerciciosValidos);
    }

    public Task<bool> EliminarRutinaAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<RutinasDto> ObtenerRutinaByIdAsync(Guid id)
    {
        var rutina = await _context.Rutinas.Include(r => r.RutinaEjercicios).ThenInclude(re => re.Ejercicio).
        FirstOrDefaultAsync(r => r.Id == id) ?? throw new KeyNotFoundException("Rutina no encontrada");
        var ejerciciosDto = rutina.RutinaEjercicios.OrderBy(re => re.Orden).Select(re => new EjerciciosDto
        (
            re.Ejercicio.Id,
            re.Ejercicio.Nombre,
            re.Ejercicio.Descripcion,
            re.Ejercicio.GrupoMuscular,
            re.Ejercicio.EstaActivo
        ));
        return MapToRutinasDto(rutina, ejerciciosDto);
    }

    public async Task<IEnumerable<RutinasDto>> ObtenerRutinasAsync()
    {
        return await _context.Rutinas.Include(r => r.RutinaEjercicios).ThenInclude(re => re.Ejercicio).Select(r => new RutinasDto
        (
            r.Id,
            r.SocioId,
            r.EntrenadorId,
            r.Nombre,
            r.Objetivo,
            r.FechaInicio,
            r.FechaFin,
            r.Activa,
            r.RutinaEjercicios.Select(re => new EjerciciosDto
            (
                re.Ejercicio.Id,
                re.Ejercicio.Nombre,
                re.Ejercicio.Descripcion,
                re.Ejercicio.GrupoMuscular,
                re.Ejercicio.EstaActivo
            ))
        )).ToListAsync();
    }

    private static RutinasDto MapToRutinasDto(Rutinas rutina,IEnumerable<EjerciciosDto> ejercicios)
    {
        return new RutinasDto
        (
            rutina.Id,
            rutina.SocioId,
            rutina.EntrenadorId,
            rutina.Nombre,
            rutina.Objetivo,
            rutina.FechaInicio,
            rutina.FechaFin,
            rutina.Activa,
            ejercicios
        );
    }
}

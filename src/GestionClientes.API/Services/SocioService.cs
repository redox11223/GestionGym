using System;
using GestionClientes.API.Data;
using GestionClientes.API.Models;
using GestionClientes.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;
namespace GestionClientes.API.Services;

public class SocioService : ISocioService
{
    private readonly GestionClientesDbContext _context;
    private readonly IMembresiaService _membresiaService;
    public SocioService(GestionClientesDbContext context, IMembresiaService membresiaService)
    {
        _context = context;
        _membresiaService = membresiaService;
    }

    public async Task<SocioDto> UpsertSocioAsync(Guid id, CreateSocioDto createSocioDto)
    {
        var socioExistente = await _context.Socios.FirstOrDefaultAsync(s => s.Id == id);
        if (socioExistente == null)
        {
            socioExistente = new Socios
            {
                Id = id,
                UsuarioId = id,
                Genero = createSocioDto.Genero,
            };
            _context.Socios.Add(socioExistente);
        }
        socioExistente.FechaNacimiento = createSocioDto.FechaNacimiento;
        socioExistente.Genero = createSocioDto.Genero;
        socioExistente.AlturaCm = createSocioDto.AlturaCm;
        socioExistente.PesoKg = createSocioDto.PesoKg;
        socioExistente.EmergenciaNombre = createSocioDto.EmergenciaNombre;
        socioExistente.EmergenciaTelefono = createSocioDto.EmergenciaTelefono;
        await _context.SaveChangesAsync();
        return MapToDto(socioExistente);
    }
    public async Task<bool> AsignarEntrenadorASocioAsync(Guid socioId, Guid entrenadorId)
    {
        var socio = await _context.Socios.Include(s => s.SocioEntrenadores).FirstOrDefaultAsync(s => s.Id == socioId)
        ?? throw new KeyNotFoundException($"No se encontró el socio con id {socioId}");

        var entrenador = await _context.Entrenadores.FirstOrDefaultAsync(e => e.Id == entrenadorId) ??
         throw new KeyNotFoundException($"No se encontró el entrenador con id {entrenadorId}");

        if (socio.SocioEntrenadores.Any(se => se.EntrenadorId == entrenadorId && se.EstaActivo))
        {
            return false;
        }

        var socioEntrenador = new SocioEntrenador
        {
            SocioId = socioId,
            EntrenadorId = entrenadorId,
            FechaAsignacion = DateOnly.FromDateTime(DateTime.Now),
            EstaActivo = true
        };

        _context.Add(socioEntrenador);
        _context.SaveChanges();

        return true;
    }
    public async Task<bool> AsignarMembresiaASocioAsync(Guid socioId, ComprarMembresiaDto comprarMembresiaDto)
    {
        if(!await ExisteSocioAsync(socioId))
        {
            throw new KeyNotFoundException($"No se encontró el socio con id {socioId}");
        }
        var membresia = await _membresiaService.ObtenerMembresiaByIdAsync(comprarMembresiaDto.MembresiaId);
        if(membresia.EstaActivo == false)
        {
            throw new InvalidOperationException($"La membresía con id {comprarMembresiaDto.MembresiaId} no está activa.");
        }

        var fechaFin = comprarMembresiaDto.FechaInicio.AddDays(membresia.DuracionDias);
        var socioMembresia = new SocioMembresia
        {
            SocioID = socioId,
            MembresiaID = comprarMembresiaDto.MembresiaId,
            FechaInicio = comprarMembresiaDto.FechaInicio,
            FechaFin = fechaFin,
            Estado = "Activa",
            MontoPagado = membresia.Precio,
            Notas = comprarMembresiaDto.Notas?? "Sin observaciones",
            FechaCreacion = DateTime.Now
        };
        await _context.SocioMembresia.AddAsync(socioMembresia);
        await _context.SaveChangesAsync();
        return true;    
    }
    public async Task<SocioDto> ActualizarSocioAsync(Guid id, CreateSocioDto updateSocioDto)
    {
        var socio = await _context.Socios.FirstOrDefaultAsync(s => s.Id == id) ?? throw new KeyNotFoundException($"No se encontró el socio con id {id}");
        socio.UsuarioId = id;
        socio.FechaNacimiento = updateSocioDto.FechaNacimiento;
        socio.Genero = updateSocioDto.Genero;
        socio.AlturaCm = updateSocioDto.AlturaCm;
        socio.PesoKg = updateSocioDto.PesoKg;
        socio.EmergenciaNombre = updateSocioDto.EmergenciaNombre;
        socio.EmergenciaTelefono = updateSocioDto.EmergenciaTelefono;
        _context.Socios.Update(socio);
        await _context.SaveChangesAsync();
        return MapToDto(socio);
    }

    public async Task<SocioDto> CrearSocioAsync(CreateSocioDto createSocioDto)
    {
        var socio = new Socios
        {
            FechaNacimiento = createSocioDto.FechaNacimiento,
            Genero = createSocioDto.Genero,
            AlturaCm = createSocioDto.AlturaCm,
            PesoKg = createSocioDto.PesoKg,
            EmergenciaNombre = createSocioDto.EmergenciaNombre,
            EmergenciaTelefono = createSocioDto.EmergenciaTelefono,
        };

        _context.Socios.Add(socio);
        await _context.SaveChangesAsync();
        return MapToDto(socio);
    }

    public async Task EliminarSocioAsync(Guid id)
    {
        var socio = await _context.Socios.FirstOrDefaultAsync(s => s.Id == id) ?? throw new KeyNotFoundException($"No se encontró el socio con id {id}");
        _context.Socios.Remove(socio);
        await _context.SaveChangesAsync();
    }

    public async Task<SocioDto> ObtenerSocioByIdAsync(Guid id)
    {
        var socio = await _context.Socios.FirstOrDefaultAsync(s => s.Id == id) ?? throw new KeyNotFoundException($"No se encontró el socio con id {id}");
        return MapToDto(socio);
    }
    public async Task<IEnumerable<SocioDto>> ObtenerTodosSociosAsync()
    {
        return await _context.Socios
            .Select(s => new SocioDto
            (
                s.Id,
                s.UsuarioId,
                s.FechaNacimiento,
                s.Genero,
                s.AlturaCm,
                s.PesoKg,
                s.EmergenciaNombre,
                s.EmergenciaTelefono,
                s.FechaCreacion
                ))
            .ToListAsync();

    }
    public async Task<bool> ExisteSocioAsync(Guid id)
    {
        return await _context.Socios.AnyAsync(s => s.Id == id);
    }
    private static SocioDto MapToDto(Socios socio)
    {
        return new SocioDto
        (
            socio.Id,
            socio.UsuarioId,
            socio.FechaNacimiento,
            socio.Genero,
            socio.AlturaCm,
            socio.PesoKg,
            socio.EmergenciaNombre,
            socio.EmergenciaTelefono,
            socio.FechaCreacion
        );
    }

}

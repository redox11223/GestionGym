using System;
using GestionClientes.API.Data;
using GestionClientes.API.Models;
using GestionClientes.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;
namespace GestionClientes.API.Services;

public class SocioService : ISocioService
{
    private readonly GestionClientesDbContext _context;
    public SocioService(GestionClientesDbContext context)
    {
        _context = context;
    }

    public async Task<SocioDto> UpsertSocioAsync(Guid id, CreateSocioDto createSocioDto)
    {
        var socioExistente = _context.Socios.FirstOrDefault(s => s.Id == id);
        if (socioExistente != null)
        {
            socioExistente.UsuarioId = createSocioDto.UsuarioId;
            socioExistente.FechaNacimiento = createSocioDto.FechaNacimiento;
            socioExistente.Genero = createSocioDto.Genero;
            socioExistente.AlturaCm = createSocioDto.AlturaCm;
            socioExistente.PesoKg = createSocioDto.PesoKg;
            socioExistente.EmergenciaNombre = createSocioDto.EmergenciaNombre;
            socioExistente.EmergenciaTelefono = createSocioDto.EmergenciaTelefono;
            socioExistente.EstaActivo = createSocioDto.EstaActivo;

            _context.Socios.Update(socioExistente);
            await _context.SaveChangesAsync();
            return MapToDto(socioExistente);
        }
        else
        {
            var socio = new Socios
            {
                UsuarioId = createSocioDto.UsuarioId,
                FechaNacimiento = createSocioDto.FechaNacimiento,
                Genero = createSocioDto.Genero,
                AlturaCm = createSocioDto.AlturaCm,
                PesoKg = createSocioDto.PesoKg,
                EmergenciaNombre = createSocioDto.EmergenciaNombre,
                EmergenciaTelefono = createSocioDto.EmergenciaTelefono,
                FechaRegistro = DateOnly.FromDateTime(DateTime.UtcNow),
                EstaActivo = true
            };

            _context.Socios.Add(socio);
            await _context.SaveChangesAsync();
            return MapToDto(socio);
        }
    }
    public async Task<SocioDto> ActualizarSocioAsync(Guid id, CreateSocioDto updateSocioDto)
    {
        var socio = await _context.Socios.FirstOrDefaultAsync(s => s.Id == id) ?? throw new KeyNotFoundException($"No se encontró el socio con id {id}");
        socio.UsuarioId = updateSocioDto.UsuarioId;
        socio.FechaNacimiento = updateSocioDto.FechaNacimiento;
        socio.Genero = updateSocioDto.Genero;
        socio.AlturaCm = updateSocioDto.AlturaCm;
        socio.PesoKg = updateSocioDto.PesoKg;
        socio.EmergenciaNombre = updateSocioDto.EmergenciaNombre;
        socio.EmergenciaTelefono = updateSocioDto.EmergenciaTelefono;
        socio.EstaActivo = updateSocioDto.EstaActivo;
        _context.Socios.Update(socio);
        await _context.SaveChangesAsync();
        return MapToDto(socio);
    }

    public async Task<SocioDto> CrearSocioAsync(CreateSocioDto createSocioDto)
    {
        var socio = new Socios
        {
            UsuarioId = createSocioDto.UsuarioId,
            FechaNacimiento = createSocioDto.FechaNacimiento,
            Genero = createSocioDto.Genero,
            AlturaCm = createSocioDto.AlturaCm,
            PesoKg = createSocioDto.PesoKg,
            EmergenciaNombre = createSocioDto.EmergenciaNombre,
            EmergenciaTelefono = createSocioDto.EmergenciaTelefono,
            FechaRegistro = DateOnly.FromDateTime(DateTime.UtcNow),
            EstaActivo = true
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
                s.FechaRegistro,
                s.EstaActivo))
            .ToListAsync();

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
            socio.FechaRegistro,
            socio.EstaActivo
        );
    }

}

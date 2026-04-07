using System;
using Autenticacion.API.Data;
using Autenticacion.API.Models.Dtos;
using Autenticacion.API.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace Autenticacion.API.Services;

public class RolService : IRolService
{
    private readonly AutenticacionDbContext _context;

    public RolService(AutenticacionDbContext context)
    {
        _context = context;
    }
    public async Task<RolDto> ActualizarRolAsync(Guid id, CreateRolDto updateRolDto)
    {
        var nombreNormalizado = updateRolDto.Nombre.Trim().ToUpper();
        if (await _context.Roles.AnyAsync(r => r.NombreNormalizado == nombreNormalizado && r.Id != id))
        {
            throw new InvalidOperationException($"Ya existe un rol con el nombre {updateRolDto.Nombre}");
        }
        var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id) ?? throw new KeyNotFoundException($"No se encontró el rol con id {id}");
        rol.Nombre = updateRolDto.Nombre.Trim();
        rol.NombreNormalizado = nombreNormalizado;
        await _context.SaveChangesAsync();
        return MapToRolDto(rol);
    }

    public async Task<RolDto> CrearRolAsync(CreateRolDto createRolDto)
    {
        var nombreNormalizado = createRolDto.Nombre.Trim().ToUpper();
        if (await _context.Roles.AnyAsync(r => r.NombreNormalizado == nombreNormalizado))
        {
            throw new InvalidOperationException($"Ya existe un rol con el nombre {createRolDto.Nombre}");
        }

        var rol = new Rol
        {
            Nombre = createRolDto.Nombre.Trim(),
            NombreNormalizado = nombreNormalizado
        };

        _context.Roles.Add(rol);
        await _context.SaveChangesAsync();

        return MapToRolDto(rol);
    }

    public async Task<IEnumerable<RolDto>> ObtenerRolesAsync()
    {
        return await _context.Roles
            .Select(r => new RolDto(r.Id, r.Nombre, r.NombreNormalizado))
            .ToListAsync();
    }


    public async Task<RolDto> RolPorIdAsync(Guid id)
    {
        var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id) ?? throw new KeyNotFoundException($"No se encontró el rol con id {id}");
        return MapToRolDto(rol);
    }

    public async Task<IEnumerable<RolDto>> ObtenerRolesValidosAsync(IEnumerable<Guid> rolIds)
    {
        return await _context.Roles
            .Where(r => rolIds.Contains(r.Id))
            .Select(r => new RolDto(r.Id, r.Nombre, r.NombreNormalizado))
            .ToListAsync();
    }
    public async Task EliminarRolAsync(Guid id)
    {
        var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id) ?? throw new KeyNotFoundException($"No se encontró el rol con id {id}");
        _context.Roles.Remove(rol);
        await _context.SaveChangesAsync();
    }
    private static RolDto MapToRolDto(Rol rol)
    {
        return new RolDto(rol.Id, rol.Nombre, rol.NombreNormalizado);

    }
}

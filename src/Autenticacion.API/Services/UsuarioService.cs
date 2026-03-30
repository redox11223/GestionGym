using System;
using Autenticacion.API.Data;
using Autenticacion.API.Models.Dtos;
using Autenticacion.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.API.Services;

public class UsuarioService : IUsuarioService
{
    private readonly AutenticacionDbContext _dbContext;
    private readonly IRolService _rolService;
    public UsuarioService(AutenticacionDbContext dbContext, IRolService rolService)
    {
        _rolService = rolService;
        _dbContext = dbContext;
    }   
    public async Task<UsuarioDto> CrearUsuarioAsync(CreateUsuarioDto createUsuarioDto)
    {
        if (await UsuarioExisteAsync(createUsuarioDto.Correo))
        {
            throw new InvalidOperationException($"Ya existe un usuario con el correo {createUsuarioDto.Correo}");
        }

        var usuario = new Usuario
        {
            Nombre = createUsuarioDto.Nombre,
            NombreNormalizado = createUsuarioDto.Nombre.Trim().ToUpper(),
            Correo = createUsuarioDto.Correo,
            CorreoNormalizado = createUsuarioDto.Correo.Trim().ToUpper(),
            ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(createUsuarioDto.Contrasena),
            Telefono = createUsuarioDto.Telefono,
            EsActivo = true,
            UltimoLogin = DateTimeOffset.UtcNow
        };

        usuario.UsuarioRoles=createUsuarioDto.Roles.Select(rolGuid => new UsuarioRol
        {
            RolId = rolGuid,
            FechaAsignacion = DateTimeOffset.UtcNow
        }).ToList();

        _dbContext.Usuarios.Add(usuario);
        await _dbContext.SaveChangesAsync();

        return MapToUsuarioDto(usuario);
    }

    public async Task<UsuarioDto> UsuarioPorIdAsync(Guid id)
    {
        var usuario = await _dbContext.Usuarios
            .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException($"No se encontró el usuario con id {id}");
        
        return MapToUsuarioDto(usuario);
    }

    public async Task<bool> UsuarioExisteAsync(string correo)
    {
        return await _dbContext.Usuarios.AnyAsync(u => u.CorreoNormalizado == correo.Trim().ToUpper());
    }

    public async Task<IEnumerable<UsuarioDto>> ObtenerUsuariosAsync()
    {
        return await _dbContext.Usuarios
            .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
            .Select(u => new UsuarioDto(
                u.Id,
                u.Nombre,
                u.Correo,
                u.Telefono,
                u.UsuarioRoles.Select(ur => ur.Rol.Nombre)
            ))
            .ToListAsync();
    }

    private static UsuarioDto MapToUsuarioDto(Usuario usuario)
    {
        return new UsuarioDto(
            usuario.Id,
            usuario.Nombre,
            usuario.Correo,
            usuario.Telefono,
            usuario.UsuarioRoles.Select(ur => ur.Rol.Nombre)
        );
    }
}

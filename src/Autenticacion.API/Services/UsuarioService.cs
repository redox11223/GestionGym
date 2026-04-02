using System;
using Autenticacion.API.Data;
using Autenticacion.API.Models;
using Autenticacion.API.Models.Dtos;
using Autenticacion.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.API.Services;

public class UsuarioService : IUsuarioService
{
    private readonly AutenticacionDbContext _dbContext;
    private readonly IRolService _rolService;
    private readonly IGestionCliente _gestionCliente;
    public UsuarioService(AutenticacionDbContext dbContext, IRolService rolService, IGestionCliente gestionCliente)
    {
        _rolService = rolService;
        _gestionCliente = gestionCliente;
        _dbContext = dbContext;
    }
    public async Task<UsuarioDetalladoDto> CrearUsuarioAsync(CreateUsuarioDto createUsuarioDto)
    {
        if (await UsuarioExisteAsync(createUsuarioDto.Correo))
        {
            throw new InvalidOperationException($"Ya existe un usuario con el correo {createUsuarioDto.Correo}");
        }

        var roles = await _rolService.ObtenerRolesValidosAsync(createUsuarioDto.Roles);
        if (roles.Count() != createUsuarioDto.Roles.Count())
        {
            throw new InvalidOperationException("Uno o más roles asignados no son válidos");
        }

        var usuario = new Usuario
        {
            Nombre = createUsuarioDto.Nombre,
            NombreNormalizado = createUsuarioDto.Nombre.Trim().ToLower(),
            Correo = createUsuarioDto.Correo,
            CorreoNormalizado = createUsuarioDto.Correo.Trim().ToLower(),
            ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(createUsuarioDto.Contrasena),
            Telefono = createUsuarioDto.Telefono,
            EsActivo = true,
            UltimoLogin = DateTimeOffset.UtcNow
        };

        usuario.UsuarioRoles = roles.Select(rol => new UsuarioRol
        {
            RolId = rol.Id,
            FechaAsignacion = DateTimeOffset.UtcNow
        }).ToList();
        var rolesNombres = roles.Select(r => r.Nombre).ToList();
        //using evita que se quede abierta la transacción en caso de error,
        // y asegura que se cierre correctamente al finalizar el bloque    
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
            CreateSocioDto? socioCreado = null;
            CreateEntrenadorDto? entrenadorCreado = null;
            if (createUsuarioDto.Socio != null && rolesNombres.Contains(RolesUsuario.Socio))
            {
                var dto = createUsuarioDto.Socio with { UsuarioId = usuario.Id };
                socioCreado= await _gestionCliente.CrearSocio(dto);
            }
            if (createUsuarioDto.Entrenador != null && rolesNombres.Contains(RolesUsuario.Entrenador))
            {
                var dto = createUsuarioDto.Entrenador with { UsuarioId = usuario.Id };
                entrenadorCreado = await _gestionCliente.CrearEntrenador(dto);
            }
            await transaction.CommitAsync();
            return MapToUsuarioDetalladoDto(usuario, socioCreado, entrenadorCreado);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException($"Error en microservicio: {ex.Message}");
        }
        
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
        return await _dbContext.Usuarios.AnyAsync(u => u.CorreoNormalizado == correo.Trim().ToLower());
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
    public async Task<UsuarioDetalladoDto> ActualizarUsuarioAsync(Guid id, CreateUsuarioDto updateUsuarioDto)
    {
        var usuario = await _dbContext.Usuarios
            .Include(u => u.UsuarioRoles)
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException($"No se encontró el usuario con id {id}");
        
        string nombreLimpio = updateUsuarioDto.Nombre.Trim().ToLower();
        string correoLimpio = updateUsuarioDto.Correo.Trim().ToLower();

        if (await _dbContext.Usuarios.AnyAsync(u => u.CorreoNormalizado == correoLimpio && u.Id != id))
        {
            throw new InvalidOperationException($"Ya existe un usuario con el correo {updateUsuarioDto.Correo}");
        }
        
        var roles = await _rolService.ObtenerRolesValidosAsync(updateUsuarioDto.Roles);
        if (roles.Count() != updateUsuarioDto.Roles.Count())
        {
            throw new InvalidOperationException("Uno o más roles asignados no son válidos");
        }
        
        var rolesNombres = roles.Select(r => r.Nombre).ToList();
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            usuario.Nombre = updateUsuarioDto.Nombre;
            usuario.NombreNormalizado = nombreLimpio;
            usuario.Correo = updateUsuarioDto.Correo;
            usuario.CorreoNormalizado = correoLimpio;
            if (!string.IsNullOrWhiteSpace(updateUsuarioDto.Contrasena))
            {
                usuario.ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(updateUsuarioDto.Contrasena);
            }
            usuario.Telefono = updateUsuarioDto.Telefono;
            usuario.UsuarioRoles = roles.Select(rol => new UsuarioRol
            {
                UsuarioId = id,
                RolId = rol.Id,
                FechaAsignacion = DateTimeOffset.UtcNow
            }).ToList();
            await _dbContext.SaveChangesAsync();
            
            //microservicios llamadas
            CreateSocioDto? socioCreado = null;
            CreateEntrenadorDto? entrenadorCreado = null;
             if (updateUsuarioDto.Entrenador != null && rolesNombres.Contains(RolesUsuario.Entrenador))
            {
                var dto = updateUsuarioDto.Entrenador with { UsuarioId = usuario.Id };
                entrenadorCreado = await _gestionCliente.UpsertEntrenador(dto);
            }
            if (updateUsuarioDto.Socio != null && rolesNombres.Contains(RolesUsuario.Socio))
            {
                var dto = updateUsuarioDto.Socio with { UsuarioId = usuario.Id };
                socioCreado = await _gestionCliente.UpsertSocio(dto);
            }

            await transaction.CommitAsync();
            return MapToUsuarioDetalladoDto(usuario, socioCreado, entrenadorCreado);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new InvalidOperationException($"Error en microservicio: {ex.Message}");
        }

        throw new NotImplementedException();

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
    private static UsuarioDetalladoDto MapToUsuarioDetalladoDto(Usuario usuario, CreateSocioDto? socio, CreateEntrenadorDto? entrenador)
    {
        return new UsuarioDetalladoDto(
            usuario.Id,
            usuario.Nombre,
            usuario.Correo,
            usuario.Telefono,
            usuario.UsuarioRoles.Select(ur => ur.Rol.Nombre),
            socio,
            entrenador
        );
    }
}

using System;
using Autenticacion.API.Models.Dtos;

namespace Autenticacion.API.Services;

public interface IGestionCliente
{
    Task<CreateSocioDto> CrearSocio(CreateSocioDto crearSocioDto);
    Task<CreateEntrenadorDto> CrearEntrenador(CreateEntrenadorDto crearEntrenadorDto);
    Task<CreateSocioDto> UpsertSocio(CreateSocioDto actualizarSocioDto);
    Task<CreateEntrenadorDto> UpsertEntrenador(CreateEntrenadorDto actualizarEntrenadorDto);
}

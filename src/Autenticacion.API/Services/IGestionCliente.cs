using System;
using Autenticacion.API.Models.Dtos;

namespace Autenticacion.API.Services;

public interface IGestionCliente
{
    Task<SocioDto> UpsertSocio(Guid id, CreateSocioDto actualizarSocioDto);
    Task<EntrenadorDto> UpsertEntrenador(Guid id, CreateEntrenadorDto actualizarEntrenadorDto);
}

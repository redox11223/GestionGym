using System;
using Autenticacion.API.Models.Dtos;

namespace Autenticacion.API.Services;

public class GestionCliente : IGestionCliente
{
    private readonly HttpClient _httpClient;
    public GestionCliente(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<CreateSocioDto> CrearSocio(CreateSocioDto crearSocioDto)
    {
        var response= await _httpClient.PostAsJsonAsync("api/socio", crearSocioDto);
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception("Error al crear socio");
        }
        var socioCreado = await response.Content.ReadFromJsonAsync<CreateSocioDto>();
        return socioCreado!;
    }
    public async Task<CreateEntrenadorDto> CrearEntrenador(CreateEntrenadorDto crearEntrenadorDto)
    {
        var response= await _httpClient.PostAsJsonAsync("api/entrenador", crearEntrenadorDto);
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception("Error al crear entrenador");
        }
        var entrenadorCreado = await response.Content.ReadFromJsonAsync<CreateEntrenadorDto>();
        return entrenadorCreado!;
    }
    public async Task<CreateSocioDto> UpsertSocio(CreateSocioDto actualizarSocioDto)
    {
        var response= await _httpClient.PutAsJsonAsync($"api/socio/upsert/{actualizarSocioDto.UsuarioId}", actualizarSocioDto);
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception("Error al actualizar socio");
        }
        var socioActualizado = await response.Content.ReadFromJsonAsync<CreateSocioDto>();
        return socioActualizado!;
    }

    public async Task<CreateEntrenadorDto> UpsertEntrenador(CreateEntrenadorDto actualizarEntrenadorDto)
    {
        var response= await _httpClient.PutAsJsonAsync($"api/entrenador/upsert/{actualizarEntrenadorDto.UsuarioId}", actualizarEntrenadorDto);
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception("Error al actualizar entrenador");
        }
        var entrenadorActualizado = await response.Content.ReadFromJsonAsync<CreateEntrenadorDto>();
        return entrenadorActualizado!;
    }

}

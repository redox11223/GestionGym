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
    
    public async Task<SocioDto> UpsertSocio(Guid id, CreateSocioDto SocioDto)
    {
        var response= await _httpClient.PutAsJsonAsync($"api/socio/upsert/{id}", SocioDto);
        if(!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al procesar socio: {response.StatusCode} - {errorContent}");
        }
        var socioActualizado = await response.Content.ReadFromJsonAsync<SocioDto>();
        return socioActualizado!;
    }

    public async Task<EntrenadorDto> UpsertEntrenador(Guid id, CreateEntrenadorDto EntrenadorDto)
    {
        var response= await _httpClient.PutAsJsonAsync($"api/entrenador/upsert/{id}", EntrenadorDto);
        if(!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error al procesar entrenador: {response.StatusCode} - {errorContent}");
        }
        var entrenadorActualizado = await response.Content.ReadFromJsonAsync<EntrenadorDto>();
        return entrenadorActualizado!;
    }

}

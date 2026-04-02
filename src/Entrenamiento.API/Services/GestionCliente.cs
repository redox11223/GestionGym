using System;

namespace Entrenamiento.API.Services;

public class GestionCliente : IGestionCliente
{
    private readonly HttpClient _httpClient;
    public GestionCliente(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task ObtenerDatosEntrenador(Guid usuarioId)
    {
        var response = await _httpClient.GetAsync($"api/entrenadores/{usuarioId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error al obtener datos del entrenador");
        }
    }

    public async Task ObtenerDatosSocio(Guid usuarioId)
    {
        var response = await _httpClient.GetAsync($"api/socios/{usuarioId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error al obtener datos del socio");
        }
    }
}

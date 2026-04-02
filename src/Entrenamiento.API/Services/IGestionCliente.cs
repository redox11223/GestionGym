using System;

namespace Entrenamiento.API.Services;

public interface IGestionCliente
{
    Task ObtenerDatosSocio(Guid usuarioId);
    Task ObtenerDatosEntrenador(Guid usuarioId);
}

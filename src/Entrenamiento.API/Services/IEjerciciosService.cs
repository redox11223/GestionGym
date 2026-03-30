using System;
using Entrenamiento.API.Models.Dtos;


namespace Entrenamiento.API.Services;

public interface IEjerciciosService
{
    Task<EjerciciosDto> AgregarEjercicios(CreateEjerciciosDto ejercicios);
    Task<EjerciciosDto> ObtenerEjercicioPorId(Guid id);
    Task<IEnumerable<EjerciciosDto>> ObtenerTodosLosEjercicios();
    Task<EjerciciosDto> ActualizarEjercicio(Guid id, CreateEjerciciosDto ejercicios);
    Task<bool> EliminarEjercicio(Guid id);
    Task<bool> ExisteEjercicio(string nombre);
}

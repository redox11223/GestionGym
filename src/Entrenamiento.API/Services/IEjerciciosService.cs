using System;
using Entrenamiento.API.Models.Dtos;
using Entrenamiento.API.Models.Entities;


namespace Entrenamiento.API.Services;

public interface IEjerciciosService
{
    Task<Ejercicios> AgregarEjercicios(CreateEjerciciosDto ejercicios);
    Task<Ejercicios> ObtenerEjercicioPorId(Guid id);
    Task<IEnumerable<Ejercicios>> ObtenerTodosLosEjercicios();
    Task<Ejercicios> ActualizarEjercicio(Guid id, CreateEjerciciosDto ejercicios);
    Task EliminarEjercicio(Guid id);
    Task<bool> ExisteEjercicio(Guid id);
}

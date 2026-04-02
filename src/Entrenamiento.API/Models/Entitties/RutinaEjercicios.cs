
namespace Entrenamiento.API.Models.Entities;

public class RutinaEjercicios
{
    public Guid RutinaId { get; set; }
    public Rutinas Rutina { get; set; } = null!;
    public int Orden { get; set; }
    public Guid EjercicioId { get; set; }
    public Ejercicios Ejercicio { get; set; } = null!;

}


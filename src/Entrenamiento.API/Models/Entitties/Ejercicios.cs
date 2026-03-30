namespace Entrenamiento.API.Models.Entities;

public class Ejercicios : EntidadBase
{
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public required string GrupoMuscular { get; set; }
    public bool EstaActivo { get; set; }
}

namespace Entrenamiento.API.Models.Entities;

public class Rutinas : EntidadBase
{
    public Guid SocioId { get; set; }
    public Guid EntrenadorId { get; set; }
    public required string Nombre { get; set; }
    public string? Objetivo { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public bool Activa { get; set; }=true;
    public ICollection<RutinaEjercicios> RutinaEjercicios { get; set; } = [];
}
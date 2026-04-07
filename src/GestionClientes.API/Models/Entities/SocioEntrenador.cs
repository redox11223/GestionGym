namespace GestionClientes.API.Models;

public class SocioEntrenador
{
    public Guid SocioId { get; set; }
    public Socios Socio { get; set; } = null!;  
    public Guid EntrenadorId { get; set; }
    public Entrenadores Entrenador { get; set; } = null!;
    public DateOnly FechaAsignacion { get; set; }
    public bool EstaActivo { get; set; }
}

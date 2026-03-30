namespace GestionClientes.API.Models;

public class SocioEntrenador
{
    public Guid SocioId { get; set; }
    public Guid EntrenadorId { get; set; }
    public DateOnly FechaAsignacion { get; set; }
    public bool EstaActivo { get; set; }
}

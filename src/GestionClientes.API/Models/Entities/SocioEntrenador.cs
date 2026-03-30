namespace GestionClientes.API.Models;

public class SocioEntrenador
{
    public int SocioId { get; set; }
    public int EntrenadorId { get; set; }
    public DateOnly FechaAsignacion { get; set; }
    public bool EstaActivo { get; set; }
}

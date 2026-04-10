namespace GestionClientes.API.Models;
public class SocioMembresia
{
    public Guid SocioMembresiaId { get; set; }
    public Guid SocioID { get; set; }
    public Guid MembresiaID { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFin { get; set;}
    public required String Estado { get; set; }
    public decimal MontoPagado { get; set; }
    public string? Notas { get; set; }
    public DateTime FechaCreacion { get; set; }

}
 
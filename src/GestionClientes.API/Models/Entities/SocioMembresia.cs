namespace GestionClientes.API.Models;
public class SocioMembresia
{
    public int SocioMembresiaId { get; set; }
    public int SocioID { get; set; }
    public int MembresiaID { get; set; }
    public DateOnly FechaInicio { get; set; }
    public DateOnly FechaFin { get; set;}
    public required String Estado { get; set; }
    public decimal MontoPagado { get; set; }
    public required string Notas { get; set; }
    public DateTime FechaCreacion { get; set; }

}

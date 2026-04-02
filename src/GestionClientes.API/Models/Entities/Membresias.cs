using GestionClientes.API.Models.Entities;

namespace GestionClientes.API.Models;

public class Membresias :EntidadBase
{
    public required string  Nombre { get; set; }
    public required string  Descripcion { get; set; }
    public int DuracionDias { get; set; }
    public decimal Precio { get; set; }
    public bool EsRenovable { get; set; }
    public bool EstaActivo { get; set; } 
}

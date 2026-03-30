using GestionClientes.API.Models.Entities;

namespace GestionClientes.API.Models;

public class Socios :EntidadBase
{
    public Guid UsuarioId { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public required string Genero { get; set; }
    public decimal AlturaCm { get; set; }
    public decimal PesoKg { get; set; }
    public required string EmergenciaNombre { get; set; }
    public required string EmergenciaTelefono { get; set; }
    public DateOnly FechaRegistro { get; set; }
    public bool EstaActivo { get; set; }

}

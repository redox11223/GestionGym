using GestionClientes.API.Models.Entities;

namespace GestionClientes.API.Models;

public class Socios :EntidadBase
{
    public Guid UsuarioId { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public required string Genero { get; set; }
    public double? AlturaCm { get; set; }
    public double? PesoKg { get; set; }
    public string? EmergenciaNombre { get; set; }
    public string? EmergenciaTelefono { get; set; }
    public ICollection<SocioEntrenador> SocioEntrenadores { get; set; } = [];
}

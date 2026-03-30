using GestionClientes.API.Models.Entities;

namespace GestionClientes.API.Models
{
public class Entrenadores :EntidadBase
{
    public int UsuarioId { get; set; }
    public required string Especialidad { get; set; } 
    public required string Certificaciones { get; set; } 
    public DateOnly FechaIngreso { get; set; }
    public bool EstaActivo { get; set; }
}
}

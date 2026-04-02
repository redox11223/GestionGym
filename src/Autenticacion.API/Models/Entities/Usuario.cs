using System;

namespace Autenticacion.API.Models.Entities;

public class Usuario: EntidadBase
{
    public required string Nombre { get; set; }
    public required string NombreNormalizado { get; set; }
    public required string Correo { get; set; }
    public required string CorreoNormalizado { get; set; }
    public required string ContrasenaHash { get; set; }
    public required string Telefono { get; set; }
    public bool EsActivo { get; set; }
    public DateTimeOffset UltimoLogin { get; set; }
    public ICollection<UsuarioRol> UsuarioRoles { get; set; } = [];    
}

using System;

namespace Autenticacion.API.Models.Entities;

public class Rol: EntidadBase
{
    public required string Nombre { get; set; }
    public required string NombreNormalizado { get; set; }
}

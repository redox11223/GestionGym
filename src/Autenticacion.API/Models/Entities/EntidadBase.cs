using System;

namespace Autenticacion.API.Models.Entities;

public abstract class EntidadBase
{
    public Guid Id { get; set; }
    public DateTimeOffset FechaCreacion { get; set; }
    public DateTimeOffset? FechaModificacion { get; set; }
    public bool isDeleted { get; set; }
}

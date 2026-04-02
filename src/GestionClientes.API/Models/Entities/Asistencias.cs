using GestionClientes.API.Models.Entities;

namespace GestionClientes.API.Models;

public class Asistencias: EntidadBase
{
    public int SocioId { get; set; }
    public DateTime FechaHoraEntrada { get; set; }
    public DateTime? FechaHoraSalida { get; set; }
    public string? Observaciones { get; set; }
    public int RegistradaPorUsuarioId { get; set; }
}

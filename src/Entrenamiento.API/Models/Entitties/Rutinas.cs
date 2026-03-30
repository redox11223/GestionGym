

namespace Entrenamiento.API.Models.Entities;

    public class Rutinas: EntidadBase
    {
    
        public int SocioId { get; set; }
        public int EntrenadorId { get; set; }
 
        public required string Nombre { get; set; }

        public string? Objetivo { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public bool Activa { get; set; }

    }

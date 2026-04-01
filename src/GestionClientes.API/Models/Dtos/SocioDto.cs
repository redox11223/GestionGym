namespace GestionClientes.API.Models.Dtos;

public record class SocioDto
(
    Guid Id,
    Guid UsuarioId,
    DateOnly FechaNacimiento,
    string Genero,
    decimal AlturaCm,
    decimal PesoKg,
    string EmergenciaNombre,
    string EmergenciaTelefono,
    DateOnly FechaRegistro,
    bool EstaActivo
);
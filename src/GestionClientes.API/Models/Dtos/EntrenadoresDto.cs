namespace GestionClientes.API.Models.Dtos;

public record class EntrenadoresDto
(
    Guid Id,
    Guid UsuarioId,
    string Especialidad,
    string Certificaciones,
    DateOnly? FechaIngreso,
    bool EstaActivo
);
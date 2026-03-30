namespace GestionClientes.API.Models.Dtos;

public record class EntrenadoresDto
(
    int Id,
    int UsuarioId,
    string Especialidad,
    string Certificaciones,
    DateOnly FechaIngreso,
    bool EstaActivo
);
namespace GestionClientes.API.Models.Dtos;

public record class MembresiasDto
(
    Guid Id,
    string Nombre,
    string Descripcion,
    int DuracionDias,
    decimal Precio,
    bool EsRenovable,
    bool EstaActivo
);
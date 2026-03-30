namespace GestionClientes.API.Models.Dtos;

public record class MembresiasDto
(
    int Id,
    string Nombre,
    string Descripcion,
    int DuracionDias,
    decimal Precio,
    bool EsRenovable,
    bool EstaActivo
);
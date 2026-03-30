namespace Entrenamiento.API.Models.Dtos;

public record class EjerciciosDto
(
    Guid Id,
    string Nombre,
    string Descripcion,
    string GrupoMuscular,
    bool EstaActivo
);

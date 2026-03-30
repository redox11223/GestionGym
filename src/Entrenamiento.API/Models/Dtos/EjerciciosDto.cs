namespace Entrenamiento.API.Models.Dtos;

public record class EjerciciosDto
(
    string Nombre,
    string Descripcion,
    string GrupoMuscular,
    bool EstaActivo
);

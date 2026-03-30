using System;
using Microsoft.AspNetCore.Mvc;
using Entrenamiento.API.Services;
using Entrenamiento.API.Models.Dtos;

namespace Entrenamiento.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class EjerciciosController : ControllerBase
{
    private readonly IEjerciciosService _ejerciciosService;

    public EjerciciosController(IEjerciciosService ejerciciosService)
    {
        _ejerciciosService = ejerciciosService;
    }

    [HttpPost]
    public async Task<ActionResult<EjerciciosDto>> AgregarEjercicio(CreateEjerciciosDto ejercicios)
    {
        try
        {
            var nuevoEjercicio = await _ejerciciosService.AgregarEjercicios(ejercicios);
            return CreatedAtAction(nameof(ObtenerEjercicioPorId), new { id = nuevoEjercicio.Id }, nuevoEjercicio);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EjerciciosDto>>> ObtenerTodosLosEjercicios()
    {
       try
        {
            var ejercicios = await _ejerciciosService.ObtenerTodosLosEjercicios();
            return Ok(ejercicios);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al obtener los ejercicios: {ex.Message}");
        }    
        
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EjerciciosDto>> ObtenerEjercicioPorId(Guid id)
    {
        try
        {
            var ejercicio = await _ejerciciosService.ObtenerEjercicioPorId(id);
            return Ok(ejercicio);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EjerciciosDto>> ActualizarEjercicio(Guid id, CreateEjerciciosDto ejercicios)
    {
        try
        {
            var ejercicioActualizado = await _ejerciciosService.ActualizarEjercicio(id, ejercicios);
            return Ok(ejercicioActualizado);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }     


    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarEjercicio(Guid id) 
    {
        try
        {
            await _ejerciciosService.EliminarEjercicio(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using GestionClientes.API.Services;
using GestionClientes.API.Models.Dtos;


namespace GestionClientes.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class MembresiasController : ControllerBase
{
    private readonly IMembresiaService _membresiaService;

    public MembresiasController(IMembresiaService membresiaService)
    {
        _membresiaService = membresiaService;
    }

    [HttpPost]
    public async Task<ActionResult<MembresiasDto>> CrearMembresia(CreateMembresiasDto membresia)
    {
        try
        {
            var nuevaMembresia = await _membresiaService.CrearMembresiaAsync(membresia);
            return CreatedAtAction(nameof(ObtenerMembresiaById), new { id = nuevaMembresia.Id }, nuevaMembresia);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembresiasDto>>> ObtenerMembresias()
    {
        try
        {
            var membresias = await _membresiaService.ObtenerMembresiasAsync();
            return Ok(membresias);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al obtener las membresías: {ex.Message}");       
        }
        
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<MembresiasDto>> ObtenerMembresiaById(Guid id)
    {
        try
        {
            var membresia = await _membresiaService.ObtenerMembresiaByIdAsync(id);
            return Ok(membresia);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<MembresiasDto>> ActualizarMembresia(Guid id, CreateMembresiasDto membresia)
    {
        try
        {
            var membresiaActualizada = await _membresiaService.ActualizarMembresiaAsync(id, membresia);
            return Ok(membresiaActualizada);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> EliminarMembresia(Guid id)
    {
        try
        {
            var resultado = await _membresiaService.EliminarMembresiaAsync(id);
            if (resultado)
            {
                return NoContent();
            }
            else
            {
                return NotFound($"No se encontró la membresía con id {id}");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al eliminar la membresía: {ex.Message}");
        }
    }
}

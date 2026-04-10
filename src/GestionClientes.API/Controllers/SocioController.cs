using GestionClientes.API.Models.Dtos;
using GestionClientes.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionClientes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class SocioController : ControllerBase
    {
        private readonly ISocioService _socioService;

        public SocioController(ISocioService socioService)
        {
            _socioService = socioService;
        }
        [HttpPost]
        public async Task<IActionResult> CrearSocio([FromBody] CreateSocioDto createSocioDto)
        {
            try
            {
                var socioCreado = await _socioService.CrearSocioAsync(createSocioDto);
                return CreatedAtAction(nameof(ObtenerSocioById), new { id = socioCreado.Id }, socioCreado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerSocioById(Guid id)  
        {
            try
            {
                var socio = await _socioService.ObtenerSocioByIdAsync(id);
                return Ok(socio);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosSocios()
        {
            var socios = await _socioService.ObtenerTodosSociosAsync();
            return Ok(socios);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarSocio(Guid id, [FromBody] CreateSocioDto updateSocioDto)
        {
            try
            {
                var socioActualizado = await _socioService.ActualizarSocioAsync(id, updateSocioDto);
                return Ok(socioActualizado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Authorize(Policy = "AdminGestion")]
        [HttpPut("upsert/{id}")]
        public async Task<IActionResult> UpsertSocio(Guid id, [FromBody] CreateSocioDto createSocioDto)
        {
            try
            {
                var socio = await _socioService.UpsertSocioAsync(id, createSocioDto);
                return Ok(socio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminGestion")]
        [HttpPost("{socioId}/asignar-entrenador/{entrenadorId}")]
        public async Task<IActionResult> AsignarEntrenadorASocio(Guid socioId, Guid entrenadorId)
        {
            try
            {
                var resultado = await _socioService.AsignarEntrenadorASocioAsync(socioId, entrenadorId);
                if (resultado)
                    return Ok(new { Message = "Entrenador asignado exitosamente al socio." });
                else
                    return BadRequest(new { Message = "No se pudo asignar el entrenador al socio." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 

        [HttpPost("{socioId}/membresia")]
        public async Task<IActionResult> ComprarMembresia(Guid socioId, [FromBody] ComprarMembresiaDto comprarMembresiaDto)
        {
            try
            {
                var resultado = await _socioService.AsignarMembresiaASocioAsync(socioId, comprarMembresiaDto);
                if (resultado)
                    return Ok(new { Message = "Membresía comprada exitosamente." });
                else
                    return BadRequest(new { Message = "No se pudo comprar la membresía." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminGestion")]
        public async Task<IActionResult> EliminarSocio(Guid id)
        {
            try
            {
                await _socioService.EliminarSocioAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

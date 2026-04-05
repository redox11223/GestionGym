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
    }
}

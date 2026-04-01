using GestionClientes.API.Models.Dtos;
using GestionClientes.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionClientes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrenadorController : ControllerBase
    {
        private readonly IEntrenadorService _entrenadorService;
        public EntrenadorController(IEntrenadorService entrenadorService)
        {
            _entrenadorService = entrenadorService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearEntrenador(CreateEntrenadoresDto createEntrenadorDto)
        {
            var entrenador = await _entrenadorService.CrearEntrenadorAsync(createEntrenadorDto);
            return CreatedAtAction(nameof(ObtenerEntrenadorById), new { id = entrenador.Id }, entrenador);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerEntrenadorById(Guid id)
        {
            var entrenador = await _entrenadorService.ObtenerEntrenadorByIdAsync(id);
            return Ok(entrenador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEntrenador(Guid id, CreateEntrenadoresDto updateEntrenadorDto)
        {
            var entrenador = await _entrenadorService.ActualizarEntrenadorAsync(id, updateEntrenadorDto);
            return Ok(entrenador);
        }

        [HttpPut("upsert/{id}")]
        public async Task<ActionResult<EntrenadoresDto>> UpsertEntrenador(Guid id, CreateEntrenadoresDto updateEntrenadorDto)
        {
            var entrenador = await _entrenadorService.UpsertEntrenadorAsync(id, updateEntrenadorDto);
            return Ok(entrenador);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEntrenador(Guid id)
        {
            await _entrenadorService.EliminarEntrenadorAsync(id);
            return NoContent();
        }
    }
}

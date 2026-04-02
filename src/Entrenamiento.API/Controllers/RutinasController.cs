using Entrenamiento.API.Models.Dtos;
using Entrenamiento.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Entrenamiento.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutinasController : ControllerBase
    {
        private readonly IRutinaService _rutinaService;

        public RutinasController(IRutinaService rutinaService)
        {
            _rutinaService = rutinaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearRutina(CreateRutinasDto createRutinasDto)
        {
            try
            {
                var rutina = await _rutinaService.CrearRutinaAsync(createRutinasDto);
                return CreatedAtAction(nameof(ObtenerRutinaById), new { id = rutina.Id }, rutina);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRutinaById(Guid id)
        {
            try
            {
                var rutina = await _rutinaService.ObtenerRutinaByIdAsync(id);
                return Ok(rutina);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRutinas()
        {
            var rutinas = await _rutinaService.ObtenerRutinasAsync();
            return Ok(rutinas);
        }
    }

}

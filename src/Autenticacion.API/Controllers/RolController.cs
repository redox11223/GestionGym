using Autenticacion.API.Models.Dtos;
using Autenticacion.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;
        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _rolService.ObtenerRolesAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolPorId(Guid id)
        {
            try
            {
                var rol = await _rolService.RolPorIdAsync(id);
                return Ok(rol);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(CreateRolDto createRolDto)
        {
            try
            {
                var rol = await _rolService.CrearRolAsync(createRolDto);
                return CreatedAtAction(nameof(GetRolPorId), new { id = rol.Id }, rol);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(Guid id, CreateRolDto updateRolDto)
        {
            try
            {
                var rol = await _rolService.ActualizarRolAsync(id, updateRolDto);
                return Ok(rol);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

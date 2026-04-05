using Autenticacion.API.Data;
using Autenticacion.API.Models.Dtos;
using Autenticacion.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IUsuarioService _usuarioService;  
        private readonly AutenticacionDbContext _dbContext;
        public AuthController(AuthService authService, IUsuarioService usuarioService, AutenticacionDbContext dbContext)
        {
            _authService = authService;
            _usuarioService = usuarioService;
            _dbContext = dbContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login( LoginDto request)
        {
            var usuario = await _dbContext.Usuarios.Include(u => u.UsuarioRoles).ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Correo == request.Correo);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Contrasena, usuario.ContrasenaHash))
            {
                return Unauthorized(new { message = "Correo o contraseña incorrectos" });
            }

            var token = _authService.GenerateJwtToken(usuario);
            return Ok(new { token });
            
        }

        [Authorize(Policy = "AdminGestion")]
        [HttpPost("register")]
        public async Task<IActionResult> CrearUsuario(CreateUsuarioDto request)
        {
            try
            {
                var usuario = await _usuarioService.CrearUsuarioAsync(request);
                return Created("Usuario creado exitosamente", usuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }
    }
}

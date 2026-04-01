using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entrenamiento.API.Controllers
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
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using UsuariosApi.Service;
namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private UsuarioService _usuarioService;

        public UsuarioController(IMapper mapper, 
            UserManager<Usuario> userManager, 
            UsuarioService usuarioService
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _usuarioService = usuarioService;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CaastroUsuario(CreateUsuarioDto dto)
        {
            await _usuarioService.CadastraAsync(dto);
            return Ok("Usuário cadastrado!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUsuarioDto dto)
            => Ok(await _usuarioService.LoginAsync(dto));

        
    }
}

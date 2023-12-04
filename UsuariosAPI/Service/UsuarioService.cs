using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Service
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signingManager;
        private TokenService _tokenService;

        public UsuarioService(IMapper mapper,
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signingManager,
            TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signingManager = signingManager;   
            _tokenService = tokenService;
        }

        public async Task CadastraAsync(CreateUsuarioDto dto)
        {
            IdentityResult result = await _userManager.CreateAsync(_mapper.Map<Usuario>
            (dto), dto.Password);

            if (!result.Succeeded)
                throw new ApplicationException(result.ToString());
        }

        public async Task<string> LoginAsync(LoginUsuarioDto dto)
        {
            var result = await _signingManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if (!result.Succeeded)
                throw new ApplicationException("Usuário não autenticado!");

            var usuario = _signingManager
                .UserManager
                .Users
                .FirstOrDefault(u => u.NormalizedUserName == dto.Username.ToUpper()); 

            return _tokenService.GenerateToken(usuario);
        }

        
    }
}

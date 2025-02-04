using apisiase.Dto;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace apisiase.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManage;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IGenericSecurityRepository<Usuario> _genericSecurityRepository;


        public UserController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManage,
            IPasswordHasher<Usuario> passwordHasher,
            ITokenService tokenService,
            RoleManager<IdentityRole> roleManager,
            IGenericSecurityRepository<Usuario> genericSecurityRepository
        )
        {
            _tokenService = tokenService;
            _genericSecurityRepository = genericSecurityRepository;
            _roleManager = roleManager;
            _signInManage = signInManage;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.FindByEmailAsync(loginDto.Email);

            if (usuario == null)
            {
                return NotFound();
            }

            var resultado = await _signInManage.CheckPasswordSignInAsync(usuario, loginDto.Password, false);

            if (!resultado.Succeeded)
            {
                return Unauthorized("No autorizado");
            }


            var roles = await _userManager.GetRolesAsync(usuario);
            foreach (var role in roles)
            {
                System.Console.WriteLine(role);

            }
            return Ok(new UsuarioDto
            {
                Nombre = usuario.Nombre,
                PrimerApellido = usuario.PrimerApellido,
                SegundoApellido = usuario.SegundoApellido,
                Id = usuario.Id,
                Email = usuario.Email,
                Token = _tokenService.CreateToken(usuario, roles),
                Admin = roles.Contains("ADMIN") ? true : false
            });
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUsuarios()
        {
            System.Console.WriteLine(User.Claims.Select(x => $"{x.Type} : {x.Value}"));
            var usuarios = await _genericSecurityRepository.GetAllAsync();

            return Ok(usuarios);
        }




    }
}

using apisiase.Dto;
using apisiase.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace apisiase.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManage;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IGenericSecurityRepository<Usuario> _genericSecurityRepository;


        public UserController(
            IMapper mapper,
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManage,
            IPasswordHasher<Usuario> passwordHasher,
            ITokenService tokenService,
            RoleManager<IdentityRole> roleManager,
            IGenericSecurityRepository<Usuario> genericSecurityRepository
        )
        {
            _mapper = mapper;
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

            return Ok(new UsuarioDto
            {
                Nombre = usuario.Nombre,
                PrimerApellido = usuario.PrimerApellido,
                SegundoApellido = usuario.SegundoApellido,
                Id = usuario.Id,
                Matricula = usuario.Matricula,
                Email = usuario.Email,
                Token = _tokenService.CreateToken(usuario, roles),
                Admin = roles.Contains("ADMIN") ? true : false,
                Alumno = roles.Contains("ALUMNO") ? true : false
            });
        }


        [HttpPost("Registro")]
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registro)
        {
            var usuario = new Usuario
            {
                Matricula = registro.Matricula,
                Nombre = registro.Nombre,
                PrimerApellido = registro.PrimerApellido,
                SegundoApellido = registro.SegundoApellido,
                Email = registro.Email,
                UserName = registro.UserName
            };

            System.Console.WriteLine(usuario);
            var result = await _userManager.CreateAsync(usuario, registro.Password);
            if (!result.Succeeded)
            {

                return BadRequest(result.Errors.ToList());
            }


            return new UsuarioDto
            {
                Nombre = usuario.Nombre,
                PrimerApellido = usuario.PrimerApellido,
                Email = usuario.Email,
                SegundoApellido = usuario.SegundoApellido,
                Token = _tokenService.CreateToken(usuario, null),
                Id = usuario.Id,
                Matricula = usuario.Matricula,
                Admin = false
            };
        }


        [Authorize]
        [HttpDelete("DeleteByID/{ID}")]
        public async Task<IActionResult> DeleteUser(string ID)
        {
            var usuario = await _userManager.FindByIdAsync(ID);

            if (usuario == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(usuario);


            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.ToList());
            }

            return Ok(result);
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPut("Role/{id}")]
        public async Task<ActionResult<UsuarioDto>> AssignRole(string id, RoleDto roleParams)
        {

            var role = await _roleManager.FindByNameAsync(roleParams.Name);
            if (role == null)
            {
                return NotFound("Role does not exists");
            }


            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound("User not found");
            }

            var usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);

            if (roleParams.Status)
            {
                var result = await _userManager.AddToRoleAsync(usuario, roleParams.Name);
                if (result.Succeeded)
                {
                    if (roleParams.Name == "ADMIN")
                    {
                        usuarioDto.Admin = true;
                    }
                    else
                    {
                        usuarioDto.Alumno = true;
                    }
                }
                if (result.Errors.Any())
                {
                    if (result.Errors.Where(x => x.Code == "UserAlreadyInRole").Any())
                    {
                        if (roleParams.Name == "ADMIN")
                        {
                            usuarioDto.Admin = true;
                        }
                        else
                        {
                            usuarioDto.Alumno = true;
                        }
                    }
                }

            }
            else
            {
                var result = await _userManager.RemoveFromRoleAsync(usuario, roleParams.Name);
                if (result.Succeeded)
                {
                    if (roleParams.Name == "ADMIN")
                    {
                        usuarioDto.Admin = false;
                    }
                    else
                    {
                        usuarioDto.Alumno = false;
                    }
                }
            }
            usuario.Alumno = usuarioDto.Alumno;
            usuario.Admin = usuarioDto.Admin;
            var resultUpdate = await _userManager.UpdateAsync(usuario);

            return usuarioDto;




        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult> GetMyProfile()
        {
            var usuario = await _userManager.BuscarUsuarioAsync(HttpContext.User);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [Authorize]
        [HttpGet("Profile")]
        public async Task<ActionResult> GetByID([FromQuery] string Id)
        {
            var usuario = await _userManager.FindByIdAsync(Id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [Authorize]
        [HttpPut("UpdateMyProfile")]
        public async Task<ActionResult> UpdateMyProfile(Usuario dataSet)
        {
            var usuario = await _userManager.BuscarUsuarioAsync(HttpContext.User);
            if (usuario == null)
            {
                return NotFound();
            }
            dataSet.Id = usuario.Id;
            var result = await _userManager.UpdateAsync(dataSet);
            if (result == null)
            {
                return BadRequest();
            }


            return Ok(dataSet);
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUserInfo([FromQuery] string userId, Usuario DataSet){

            DataSet.Id = userId;
            var result = await _userManager.UpdateAsync(DataSet);
            if(result == null){
                return BadRequest();
            }

            return Ok(DataSet);

        }




        [Authorize(Roles = "ADMIN")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUsuarios()
        {
            System.Console.WriteLine(User.Claims.Select(x => $"{x.Type} : {x.Value}"));
            var usuarios = await _genericSecurityRepository.GetAllAsync();

            return Ok(usuarios);
        }




    }
}

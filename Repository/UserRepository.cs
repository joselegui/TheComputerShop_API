using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheComputerShop.DATA;
using TheComputerShop.Models;
using TheComputerShop.Models.DTO;
using TheComputerShop.Repository.IRepository;
using XAct.Library.Settings;

namespace TheComputerShop.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<AppUser> _userManager;
        private string claveSecreta;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext db, IConfiguration config, RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager, IMapper mapper)
        {
            _db = db;
            claveSecreta = config.GetValue<string>("ApiSettings:Secret");
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public AppUser GetUsuario(string usuarioId)
        {
            return _db.AppUser.FirstOrDefault(u => u.Id == usuarioId);
        }

        public ICollection<AppUser> GetUsuarios()
        {
            return _db.AppUser.OrderBy(u => u.UserName).ToList();
        }

        public bool IsUniqueUser(string nombre)
        {
            var usuariobd = _db.AppUser.FirstOrDefault(u => u.UserName == nombre);

            if (usuariobd == null)
            {
                return true;
            }

            return false;
        }

        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {

            var usuario = _db.AppUser.FirstOrDefault(u => u.UserName.ToLower() ==
            usuarioLoginDto.NombreUsuario.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(usuario, usuarioLoginDto.Password);
            //Validamos si el usuario no existe con la combinación de usuario y contraseña correcta
            if (usuario == null || isValid == false)
            {
                return new UsuarioLoginRespuestaDto { Token = "", Usuario = null };
            }

            //Aquí existe el usuario, entonces podemos procesar el login
            var roles = await _userManager.GetRolesAsync(usuario);

            var manejadorToken = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = _mapper.Map<UsuarioDatosDto>(usuario)
            };

            return usuarioLoginRespuestaDto;
        }

        public async Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            AppUser usuario = new AppUser()
            {
                UserName = usuarioRegistroDto.NombreUsuario,
                Email = usuarioRegistroDto.NombreUsuario,
                NormalizedEmail = usuarioRegistroDto.NombreUsuario.ToUpper(),
                Name = usuarioRegistroDto.Nombre,
            };

            var resultado = await _userManager.CreateAsync(usuario, usuarioRegistroDto.Password);

            if (resultado.Succeeded)
            {
                //Solo la primera vez, y es para crear los roles
                if (!_roleManager.RoleExistsAsync("Super Administrator").GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(usuarioRegistroDto.Role));
                }

                await _userManager.AddToRoleAsync(usuario, usuarioRegistroDto.Role);
                var usuarioRetornado = _db.AppUser.FirstOrDefault(u => u.UserName == usuarioRegistroDto.NombreUsuario);

                return _mapper.Map<UsuarioDatosDto>(usuarioRetornado);
            }

            return new UsuarioDatosDto();
        }

        public bool DeleteUser(AppUser itemUsuario)
        {
            _db.AppUser.Remove(itemUsuario);

            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}

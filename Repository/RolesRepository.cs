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
    public class RolesRepository : IRolesRepository
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<UserAspRol> _userManager;
        private string claveSecreta;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RolesRepository(ApplicationDbContext db, IConfiguration config, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _db = db;
            claveSecreta = config.GetValue<string>("ApiSettings:Secret");
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public ICollection<UserAspRol> GetRoles()
        {
            return _db.UserAspRol.OrderBy(r => r.NormalizedName).ToList();
        }
    }
}

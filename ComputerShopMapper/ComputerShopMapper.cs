using AutoMapper;
using TheComputerShop.Models;
using TheComputerShop.Models.DTO;

namespace TheComputerShop.ComputerShopMapper
{
    public class ComputerShopMapper : Profile
    {

        public ComputerShopMapper()
        {
            CreateMap<AppUser,UsuarioDatosDto>().ReverseMap();
            CreateMap<AppUser, UsuarioDto>().ReverseMap();
            CreateMap<Articles, ArticleDto>().ReverseMap();
            CreateMap <Manufacturers, ManufacturersDto>().ReverseMap();
            CreateMap <UserAspRol, RolesDto>().ReverseMap();
        }
    }
}

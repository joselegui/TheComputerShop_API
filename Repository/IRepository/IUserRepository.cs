using TheComputerShop.Models;
using TheComputerShop.Models.DTO;

namespace TheComputerShop.Repository.IRepository
{
    public interface IUserRepository
    {

        //ICollection<Usuario> GetUsuarios();
        ICollection<AppUser> GetUsuarios();

        //Usuario GetUsuario(int usuarioId);
        AppUser GetUsuario(string usuarioId);

        bool IsUniqueUser(string nombre);

        bool ExistUser(int id);
        
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);

        Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto);

        //Task<bool> UpdateUser(Usuario usuario);
    }
}

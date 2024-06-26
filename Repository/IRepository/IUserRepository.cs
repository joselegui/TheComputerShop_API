﻿using TheComputerShop.Models;
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
        
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);

        Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto);
        bool DeleteUser(AppUser itemUsuario);

        //Task<bool> UpdateUser(Usuario usuario);
    }
}

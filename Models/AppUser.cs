using Microsoft.AspNetCore.Identity;

namespace TheComputerShop.Models
{
    public class AppUser : IdentityUser  
    {
        //Añadir campos personalizados
        public string Name { get; set; }
    }
}

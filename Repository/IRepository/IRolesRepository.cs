using TheComputerShop.Models;
using TheComputerShop.Models.DTO;

namespace TheComputerShop.Repository.IRepository
{
    public interface IRolesRepository
    {
        ICollection<UserAspRol> GetRoles();
    }
}

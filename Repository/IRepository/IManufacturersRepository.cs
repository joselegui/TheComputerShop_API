using TheComputerShop.Models;

namespace TheComputerShop.Repository.IRepository
{
    public interface IManufacturersRepository
    {

        Manufacturers GetManufacturers(int codeId);

        bool CreateManufacture(Manufacturers manufacturers);

        bool UpdateManufacture(Manufacturers manufacturers);

        bool ExistManufacture(string name);

        ICollection<Manufacturers> GetManufacturers();

        ICollection<Manufacturers> SearchManufacturers(string name);

        bool Save();
    }
}

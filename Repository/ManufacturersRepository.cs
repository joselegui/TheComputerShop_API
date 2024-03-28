using Microsoft.EntityFrameworkCore;
using TheComputerShop.DATA;
using TheComputerShop.Models;
using TheComputerShop.Repository.IRepository;

namespace TheComputerShop.Repository
{
    public class ManufacturersRepository : IManufacturersRepository
    {
        private readonly ApplicationDbContext _db;


        public ManufacturersRepository(ApplicationDbContext db)
        {
           _db = db;
        }

        public Manufacturers GetManufacturers(int id)
        {
            return _db.Manufacturers.FirstOrDefault(m => m.Id.Equals(id));
        }

        public bool CreateManufacture(Manufacturers manufacturers)
        {
            _db.Manufacturers.Add(manufacturers);

            return Save();
        }

        public bool ExistManufacture(string name)
        {
            bool exist = _db.Manufacturers.Any(m => m.Name.ToLower().Trim() == name.ToLower().Trim());

            return exist;
        }

        public ICollection<Manufacturers> GetManufacturers()
        {
            return _db.Manufacturers.OrderBy(m => m.Name).ToList();
        }

        public ICollection<Manufacturers> SearchManufacturers(string name)
        {
            IQueryable<Manufacturers> query = _db.Manufacturers;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(m => m.Name.Contains(name));
            }

            return query.ToList();
        }

        public bool UpdateManufacture(Manufacturers manufacturers)
        {
            _db.Manufacturers.Update(manufacturers);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}

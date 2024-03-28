using System.ComponentModel.DataAnnotations;

namespace TheComputerShop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string NameUser { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

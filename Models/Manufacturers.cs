using System.ComponentModel.DataAnnotations;

namespace TheComputerShop.Models
{
    public class Manufacturers
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
